using AutoMapper;
using System.Linq;
using Aumc.Core.Models;
using Aumc.Controllers.ApiResources;

namespace Aumc.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Domain to apiResource
            CreateMap<User, UserDto>()
                .ForMember(uDto => uDto.Url, opt => 
                    opt.MapFrom(u => u.Photos.SingleOrDefault(p => p.IsMain == true).Url));
            CreateMap<Post, PostDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<PostComment, PostCommentDto>();
            CreateMap<GroupNewsLetter, StudentGroupNewsLetterDto>();
            CreateMap<Post, SavePostDto>();
            CreateMap<UniversityNews, StudentGroupNewsLetterDto>();
            CreateMap<StudentGroupAttendance, AttendanceReturnDto>();
            CreateMap<StudentGroup, GroupDto>();
            CreateMap<StudentGroup, StudentGroupDto>();
            CreateMap<User, BlockUserDto>();
            CreateMap<SaveUniversityNewsDto, UniversityNews>();

            CreateMap<StudentGroupPost, PostDto>();
            
            CreateMap<StudentGroupComment, CommentDto>();
            CreateMap<Group, SaveGroupDto>();
            CreateMap<Following, FollowingDto>();
            CreateMap<SaveStudentGroupDto, StudentGroup>();
            CreateMap<StudentGroupAttendance, StudentGroupPendingAttendanceDto>();
            CreateMap<StudentGroupPost, StudentGroupPostDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<Comment, CommentDto>()
                .ForPath(cDto => cDto.Id, opt => opt.MapFrom(c => c.Id))
                .ForPath(cDto => cDto.User.Id, opt => opt.MapFrom(c => c.User.Id))
                .ForPath(cDto => cDto.User.KnownAs, opt => opt.MapFrom(c => c.User.KnownAs))
                .ForPath(cDto => cDto.User.LastActive, opt => opt.MapFrom(c => c.User.LastActive))
                .ForPath(cDto => cDto.User.TeacherOrStudent, opt => opt.MapFrom(c => c.User.TeacherOrStudent))
                .ForPath(cDto => cDto.Message, opt => opt.MapFrom(c => c.Message))
                .ForPath(cDto => cDto.User.Url, opt => opt.MapFrom(c => c.User.Photos.SingleOrDefault(p => p.IsMain == true).Url));

            CreateMap<StudentGroupNewsLetter, StudentGroupNewsLetterDto>();  
            CreateMap<Interest, InterestDto>();
            CreateMap<BlockUser, BlockUserDto>()
                .ForMember(buDto => buDto.Url, opt => 
                    opt.MapFrom(bu => bu.User.Photos.SingleOrDefault(p => p.IsMain == true).Url));
            
            CreateMap<GroupType, GroupTypeDto>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<UserReport, UserReportDto>()
                .ForPath(urd => urd.Reportee.Id, opt => opt.MapFrom(ur => ur.Reportee.Id))
                .ForPath(urd => urd.Reportee.KnownAs, opt => opt.MapFrom(ur => ur.Reportee.KnownAs))
                .ForPath(urd => urd.Reportee.LastActive, opt => opt.MapFrom(ur => ur.Reportee.LastActive))
                .ForPath(urd => urd.Reportee.TeacherOrStudent, opt => opt.MapFrom(ur => ur.Reportee.TeacherOrStudent))
                .ForPath(urd => urd.Reportee.Url, opt => opt.MapFrom(ur => ur.Reportee.Photos.SingleOrDefault(p => p.IsMain == true).Url))
                .ForPath(urd => urd.Reporter.Id, opt => opt.MapFrom(ur => ur.Reporter.Id))
                .ForPath(urd => urd.Reporter.KnownAs, opt => opt.MapFrom(ur => ur.Reporter.KnownAs))
                .ForPath(urd => urd.Reporter.LastActive, opt => opt.MapFrom(ur => ur.Reporter.LastActive))
                .ForPath(urd => urd.Reporter.TeacherOrStudent, opt => opt.MapFrom(ur => ur.Reporter.TeacherOrStudent))
                .ForPath(urd => urd.Reporter.Url, opt => opt.MapFrom(ur => ur.Reporter.Photos.SingleOrDefault(p => p.IsMain == true).Url));

            CreateMap<PostCategory, PostCategoryDto>();

            CreateMap<Attendance, AttendanceReturnDto>()
                .ForPath(at => at.Group.Id, opt => opt.MapFrom(a => a.Group.Id))
                .ForPath(at => at.Group.Subject, opt => opt.MapFrom(a => a.Group.Subject))
                .ForPath(at => at.Group.ClassName, opt => opt.MapFrom(a => a.Group.ClassName))
                .ForPath(at => at.Group.Section, opt => opt.MapFrom(a => a.Group.Section))
                .ForPath(at => at.Group.Description, opt => opt.MapFrom(a => a.Group.Description))
                .ForPath(at => at.Group.User.Id, opt => opt.MapFrom(a => a.Group.User.Id))
                .ForPath(at => at.Group.User.KnownAs, opt => opt.MapFrom(a => a.Group.User.KnownAs))
                .ForPath(at => at.Group.User.TeacherOrStudent, opt => opt.MapFrom(a => a.Group.User.TeacherOrStudent))
                .ForPath(at => at.Group.User.Url, opt => opt.MapFrom(a => a.Group.User.Photos.SingleOrDefault(p => p.IsMain == true).Url));

            CreateMap<User, UserListDto>()
                .ForMember(des => des.Age, opt => opt.MapFrom(src => src.GetAge))
                .ForMember(des => des.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForPath(des => des.Address.City, opt => opt.MapFrom(src => src.Address.City))
                .ForPath(des => des.Address.Province, opt => opt.MapFrom(src => src.Address.Province))
                .ForPath(des => des.Address.Country, opt => opt.MapFrom(src => src.Address.Country));
            
            CreateMap<User, UserDetailsDto>()
                .ForMember(des => des.Age, opt => opt.MapFrom(src => src.GetAge))
                .ForMember(des => des.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForPath(des => des.Address.City, opt => opt.MapFrom(src => src.Address.City))
                .ForPath(des => des.Address.Province, opt => opt.MapFrom(src => src.Address.Province))
                .ForPath(des => des.Address.Country, opt => opt.MapFrom(src => src.Address.Country));
            
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => 
                    opt.MapFrom(m => m.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt => 
                    opt.MapFrom(m => m.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));

            CreateMap<Attendance, GroupAttendanceReturnDto>()
                .ForPath(groupDto => groupDto.User.Id, opt => opt.MapFrom(a => a.User.Id))
                .ForPath(groupDto => groupDto.User.KnownAs, opt => opt.MapFrom(a => a.User.KnownAs))
                .ForPath(groupDto => groupDto.User.LastActive, opt => opt.MapFrom(a => a.User.LastActive))
                .ForPath(groupDto => groupDto.User.TeacherOrStudent, opt => opt.MapFrom(a => a.User.TeacherOrStudent))
                .ForPath(groupDto => groupDto.User.Url, opt => 
                    opt.MapFrom(a => a.User.Photos.SingleOrDefault(p => p.IsMain == true).Url));

            CreateMap<Attendance, PendingAttendanceDto>()
                .ForPath(pDto => pDto.User.Id, opt => opt.MapFrom(a => a.User.Id))
                .ForPath(pDto => pDto.User.KnownAs, opt => opt.MapFrom(a => a.User.KnownAs))
                .ForPath(pDto => pDto.User.LastActive, opt => opt.MapFrom(a => a.User.LastActive))
                .ForPath(pDto => pDto.User.TeacherOrStudent, opt => opt.MapFrom(a => a.User.TeacherOrStudent))
                .ForPath(pDto => pDto.User.Url, opt => opt.MapFrom(a => a.User.Photos.SingleOrDefault(p => p.IsMain == true).Url));
            
            // ApiResource to Domain
            CreateMap<PostDto, Post>();
            CreateMap<SavePostDto, Post>();
            CreateMap<AddressDto, Address>();
            CreateMap<UseRegisterDto, User>();
            CreateMap<InterestDto, Interest>();
            CreateMap<SaveCommentDto, Comment>();
            CreateMap<SaveStudentGroupNewsLetterDto, GroupNewsLetter>();
            CreateMap<PhotoForReturnDto, Photo>();
            CreateMap<StudentGroupNewsLetterDto, GroupNewsLetter>();
            CreateMap<FileForCreationDto, Post>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<SavePostCommentDto, StudentGroupPostComment>();
            CreateMap<SavePostCommentDto, PostComment>();
            CreateMap<StudentGroupPostComment, PostCommentDto>();
            CreateMap<SaveUserReportDto, UserReport>();
            CreateMap<SaveCommentDto, StudentGroupComment>();
            CreateMap<SaveStudentGroupPostDto, StudentGroupPost>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<StudentGroupNewsLetterDto, StudentGroupNewsLetter>();
            CreateMap<StudentGroupAttendance, GroupAttendanceReturnDto>();
            CreateMap<SaveStudentGroupAttendanceDto, StudentGroupAttendance>();
            CreateMap<SaveStudentGroupNewsLetterDto, StudentGroupNewsLetter>();
            CreateMap<StudentGroupNewsLetterDto, UniversityNews>();           
            CreateMap<UserUpdateDto, User>()
                .ForMember(des => des.Id, opt => opt.Ignore());
            
            CreateMap<SaveGroupDto, Group>()
                .ForMember(g => g.Id, opt => opt.Ignore());
        }
    }
}