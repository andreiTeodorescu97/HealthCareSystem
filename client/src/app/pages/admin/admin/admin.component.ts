import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { gridSettings } from 'app/_models/grid';
import { AdminUserDto } from 'app/_models/_admin/adminUserDto';
import { AdminService } from 'app/_services/admin.service';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit, OnDestroy {

  usersWithRoles: AdminUserDto[] = [];
  dtOptions: DataTables.Settings = {};
  isVisible = false;

  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective;

  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private adminService: AdminService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeGrid();
  }

  initializeGrid() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 50,
      searching: false,
      /*       columnDefs: [
              { orderable: false, targets: 5 },
            ], */
      language: gridSettings,

    };

    this.getUsers();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();
      this.getUsers();
    });
  }

  getUsers() {
    this.adminService.getUserWithRoles()
      .subscribe(data => {
        this.usersWithRoles = data;
        this.dtTrigger.next();
      });
  }

  block(userName: string) {
    this.adminService.block(userName).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Contul a fost blocat cu succes!!</span>',
        "Cont",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.rerender();
    });
  }

  unblock(userName: string) {
    this.adminService.unblock(userName).subscribe(() => {
      this.toastr.success(
        '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">Contul a fost deblocat cu succes!!</span>',
        "Cont",
        {
          toastClass: "alert alert-success alert-with-icon",
        }
      );
      this.rerender();
    });
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
}
