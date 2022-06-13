import { AuthService } from './../_services/auth.service';
import { AlertifyService } from './../_services/alertify.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { UniversityNewsService } from '../_services/university-news.service';

@Component({
  selector: 'app-university-news',
  templateUrl: './university-news.component.html',
  styleUrls: ['./university-news.component.css']
})
export class UniversityNewsComponent implements OnInit {

  oneAtATime: boolean = true;

  createNews: any = {};
  modalRef: BsModalRef;
  groupNews: any[] = [];
  groupNewsService: any;

  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private modalService: BsModalService,
    private newsService: UniversityNewsService) { }

  ngOnInit() {

    console.log(this.authService.decodedToken);

    this.newsService.getNews()
      .subscribe(groupNews => this.groupNews = groupNews)
  }

  openModalForNews(news: TemplateRef<any>) {
    this.createNews = {
      id: 0,
      title: '',
      description: ''
    };
    this.modalRef = this.modalService.show(news);    
  }

  openModalForNewsUpdate(updateNews: any, news: any) {
    this.createNews = news
    this.modalRef = this.modalService.show(updateNews);    
  }

  saveNews() {
    if (this.createNews.id == 0) {
      this.newsService.addNews(this.createNews)
        .subscribe(news => {
          this.alertify.success('news added');
          this.groupNews.push(news);
          this.modalRef.hide();
          this.createNews = {};
        },
        () => this.alertify.error('something went wrong'));
    } else {
        const index = this.groupNews.findIndex(n => n.id == this.createNews.id);

        this.newsService.updateNews(this.createNews)
          .subscribe(news => {
            this.alertify.success('news updated')
            this.groupNews.splice(index, 1, news);
            this.modalRef.hide();
          },
          () => this.alertify.error('something went wrong'));
    }
  }

  removeNews(newsId) {
    this.alertify.confirm('Are you sure you want to delete this news ?', () => {
      const index = this.groupNews.findIndex(n => n.id == newsId);

      this.newsService.deleteNews(newsId)
        .subscribe(() => {
          this.groupNews.splice(index, 1);
          this.alertify.success('news deleted');
        },
        () => this.alertify.error('something went wrong'));
    });
  }
}
