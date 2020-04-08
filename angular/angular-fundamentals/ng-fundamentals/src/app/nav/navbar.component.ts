import { Component, OnInit } from "@angular/core";
import { AuthService } from "../user/auth.service";
import { EventService, ISession, IEvent } from "../events";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "nav-bar",
  templateUrl: "./nav-bar.component.html",
  styles: [
    `
      .nav.navbar-nav {
        font-size: 15px;
      }
      #searchForm {
        margin-right: 100px;
      }
      @media (max-width: 1200px) {
        #searchForm {
          display: none;
        }
      }
      li > a.active {
        color: #f97924;
      }
    `,
  ],
})
export class NavBarComponent implements OnInit {
  searchTerm = "";
  foundSessions: ISession[];
  events: IEvent[];

  constructor(
    public authService: AuthService,
    private eventService: EventService
  ) {}

  ngOnInit(): void {
    this.eventService.getEvents().subscribe((result) => (this.events = result));
  }

  searchSessions(searchTerm) {
    this.eventService.searchSessions(searchTerm).subscribe((sessions) => {
      this.foundSessions = sessions;
    });
  }
}
