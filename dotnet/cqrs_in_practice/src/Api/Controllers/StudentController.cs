using Logic.Dtos;
using Logic.Students;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly Messages _messages;

        public StudentController(Messages messages)
        {
            _messages = messages;
        }

        [HttpGet]
        public IActionResult GetList(string enrolled, int? number)
        {
            var result = _messages.Dispatch(new GetListQuery(enrolled, number));
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Register([FromBody] NewStudentDto dto)
        {
            var command = new RegisterCommand(dto.Name, dto.Email, dto.Course1, dto.Course1Grade, dto.Course2, dto.Course2Grade);
            var result = _messages.Dispatch(command);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Unregister(long id)
        {
            var command = new UnregisterCommand(id);
            var result = _messages.Dispatch(command);
            return FromResult(result);
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, [FromBody] StudentEnrollmentDto dto)
        {
            var command = new EnrollCommand(id, dto.Course, dto.Grade);
            var result = _messages.Dispatch(command);
            return FromResult(result);
        }

        [HttpPut("{id}/enrollments/{enrollmentNumber}")]
        public IActionResult Transfer(long id, int enrollmentNumber, [FromBody] StudentTransferDto dto)
        {
            var command = new TransferCommand(id, enrollmentNumber, dto.Course, dto.Grade);
            var result = _messages.Dispatch(command);
            return FromResult(result);
        }

        [HttpPut("{id}/enrollments/{enrollmentNumber}/deletion")]
        public IActionResult Disenroll(long id, int enrollmentNumber, [FromBody] StudentDisenrollmentDto dto)
        {
            var command = new DisenrollCommand(id, enrollmentNumber, dto.Comment);
            var result = _messages.Dispatch(command);
            return FromResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(long id, [FromBody] StudentPersonalInfoDto dto)
        {
            var command = new EditPersonalInfoCommand(id, dto.Name, dto.Email);
            var result = _messages.Dispatch(command);
            return FromResult(result);
        }
    }
}