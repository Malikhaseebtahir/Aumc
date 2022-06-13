import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AdminService } from './../../_services/admin.service';
import { AlertifyService } from './../../_services/alertify.service';

@Component({
  selector: 'app-report-management',
  templateUrl: './report-management.component.html',
  styleUrls: ['./report-management.component.css']
})
export class ReportManagementComponent implements OnInit {
  
  reportDetails: any = {};
  modalRef: BsModalRef;
  reports: any[] = [];
  
  constructor(
    private alertify: AlertifyService,
    private adminService: AdminService,
    private modalService: BsModalService) { }

  ngOnInit() {
    this.adminService.getReport()
      .subscribe(reports => this.reports = reports);
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  openModalForReportee(reportee: TemplateRef<any>) {
    this.modalRef = this.modalService.show(reportee);
  }

  openModalForApprove(approve: TemplateRef<any>) {
    this.modalRef = this.modalService.show(approve);
  }

  approveReport(reportId: number) {
    let report = this.reports.indexOf(reportId);
    this.modalRef.hide();

    this.reportDetails.reportId = reportId;
    debugger;

    this.adminService.approveReport(this.reportDetails)
    .subscribe(() => {
      this.reports.splice(report, 1);
      this.alertify.success('successfully approved the report');
    },
    error => this.alertify.error(error));
  }
  
  rejectReport(reportId: number) {
    let report = this.reports.indexOf(reportId);
      this.adminService.rejectReport(reportId)
      .subscribe(() => {
        this.reports.splice(report, 1);
        this.alertify.success('successfully reject the report');
      },
      error => this.alertify.error(error));
  }
}
