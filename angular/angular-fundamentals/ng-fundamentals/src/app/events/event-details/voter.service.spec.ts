import { VoterService } from "./voter.service";
import { of } from "rxjs";
import { ISession } from "../shared";

describe("VoterSerice", () => {
  let voterSerice: VoterService;
  let mockHttp: any;

  beforeEach(() => {
    mockHttp = jasmine.createSpyObj("mockHttp", ["delete", "post"]);
    voterSerice = new VoterService(mockHttp);
  });

  describe("deleteVoter", () => {
    it("should remove voter from the list of voters", () => {
      let session = { id: 6, voters: ["joe", "john"] };
      mockHttp.delete.and.returnValue(of(false));

      voterSerice.deleteVoter(3, <ISession>session, "joe");

      expect(session.voters.length).toBe(1);
      expect(session.voters[0]).toBe("john");
    });

    it("should call http.delete with right URL", () => {
      let session = { id: 6, voters: ["joe", "john"] };
      mockHttp.delete.and.returnValue(of(false));

      voterSerice.deleteVoter(3, <ISession>session, "joe");

      expect(mockHttp.delete).toHaveBeenCalledWith(
        "/api/events/3/sessions/6/voters/joe"
      );
    });
  });

  describe("addVoter", () => {
    it("should call http.post with right URL", () => {
      let session = { id: 6, voters: ["john"] };
      mockHttp.post.and.returnValue(of(false));

      voterSerice.addVoter(3, <ISession>session, "joe");

      expect(mockHttp.post).toHaveBeenCalledWith(
        "/api/events/3/sessions/6/voters/joe",
        {},
        jasmine.any(Object)
      );
    });
  });
});
