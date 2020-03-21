import { Component, OnInit, Input } from "@angular/core";
import { ISession } from "../shared";

@Component({
  selector: "session-list",
  templateUrl: "session-list.component.html"
})
export class SessionListComponent {
  constructor() {}

  @Input() sessions: ISession[];
}
