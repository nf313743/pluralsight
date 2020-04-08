import { Resolve } from "@angular/router";
import { EventService } from "./shared/event.service";

export class EventListResolver implements Resolve<any> {
  constructor(private eventService: EventService) {}

  resolve() {
    return this.eventService.getEvents();
  }
}
