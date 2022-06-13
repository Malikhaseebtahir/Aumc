using Aumc.Core;
using AutoMapper;
using Aumc.Core.Models;
using CloudinaryDotNet;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Aumc.Controllers.ApiResources;

namespace Aumc.Controllers
{
    [Route("/api/{postId}/posts")]
    [ApiController]
    public class FileUploadController : Controller
    {
        private Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;

        public FileUploadController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserRepository repository,
            IUserRepository userRepository,
            IPostRepository postRepository,
            IGroupRepository groupRepository,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _groupRepository = groupRepository;
            _cloudinaryConfig = cloudinaryConfig;

            var account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(int postId,
            [FromForm]FileForCreationDto fileForCreationDto)
        {
            var postFromRepo = await _postRepository.GetPostAsync(postId);

            var file = fileForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            postFromRepo.Url = uploadResult.Uri.ToString();
            postFromRepo.PublicId = uploadResult.PublicId;

            var group = await _groupRepository.GetGroupAsync(postFromRepo.GroupId, includeRelated: false);
            if (group == null)
                return NotFound("Group not found");
                
            group.SendPostUpdatedNotification(await _userRepository.GetUsersForNotification(group.Id), postFromRepo);
            
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}