using ExamSeatingArrangement2020.Dtos;
using ExamSeatingArrangement2020.Repository;
using ExamSeatingArrangement2020.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SeatingController : ControllerBase
    {
        private readonly ISeatingRepository _seatingRepository;
        private readonly IIntermediateRepository _intermediateRepository;
        private readonly ISeatingCore _seatingCore;
        private readonly INoticeBoardService _noticeBoardService;

        public SeatingController(ISeatingRepository seatingRepository,
            IIntermediateRepository intermediateRepository,
            ISeatingCore seatingCore,
            INoticeBoardService noticeBoardService)

        {
            _seatingRepository = seatingRepository;
            _intermediateRepository = intermediateRepository;
            _seatingCore = seatingCore;
            _noticeBoardService = noticeBoardService;
        }

        [HttpGet("{date}/{session}")]
        public async Task<IActionResult> GetSeatings(string date, string session)
        {
            var seating = await _seatingRepository.GetSeatings(date, session);
            return Ok(seating);
        }

        [HttpGet("forallocation/{date}/{session}")]
        public async Task<IActionResult> GetSeatingsForAllocation(string date, string session)
        {
            var seatingallocationdetails = await _seatingRepository.getDetailsForAllocation(date, session);
            return Ok(seatingallocationdetails);
        }

        /*  [HttpGet("rectanglemodels")]
          public async Task<IActionResult> GetRectangleModel()
          {
              var rectangles = await _seatingRepository.GetRectangleModels();             Maybe used later
              return Ok(rectangles);
          }*/

        /*[HttpGet("fill")]
        public async Task<IActionResult> FillIntermediateTables()
        {
            var data = await _intermediateRepository.FillInterMediateTables();
            return Ok(data);
        }*/

        [HttpGet("departments/{date}/{session}")]
        public async Task<IActionResult> GetDepartmentsAsync(string date, string session)
        {
            Console.WriteLine(session);
            var data = await _seatingRepository.GetDepartmentDetails(date, session);
            return Ok(data);
        }

        [HttpGet("initial/{date}/{session}")]
        public async Task<IActionResult> TestFunction(string date, string session)
        {
            var data = await _seatingRepository.GetDeparmtmentDetailsWithCount(date, session);
            return Ok(data);
        }

        [HttpGet("examdetails/{date}/{session}")]
        public async Task<IActionResult> ExamDetails(string date, string session)
        {
            var data = await _seatingRepository.GetExamDetails(date, session);

            return Ok(data);
        }

        [HttpGet("examdetails")]
        public async Task<IActionResult> GetExams()
        {
            var res = await _seatingRepository.GetAllExams();
            return Ok(res);
        }

        [HttpGet("examdates/{session}")]
        public async Task<IActionResult> ExamDates(string session)
        {
            var data = await _seatingRepository.GetAllExamDates(session);
            return Ok(data);
        }

        [HttpGet("examdates")]
        public async Task<IActionResult> GetAllExamDates()
        {
            var data = await _seatingRepository.AllExamDates();
            return Ok(data);
        }

        [HttpGet("examdateswithsession")]
        public async Task<IActionResult> GetAllExamDatesWithSession()
        {
            var data = await _seatingRepository.GetExamDateswithSession();
            return Ok(data);
        }

        [HttpPost("examorder")]
        public async Task<IActionResult> OrderSetter([FromBody] Dictionary<int, int> data)
        {
            var res = await _seatingRepository.SetOrders(data);

            return Ok(res);
        }

        [HttpGet("seatingpreview")]
        public IActionResult GetSeatingPreview()
        {
            _seatingRepository.GetSeatingPreview();

            return Ok(new { res = "Success" });
        }

        [HttpGet("seatingpreviewnew/{date}/{session}")]
        public IActionResult GetSeating(string date, string session)
        {
            var result = _seatingCore.newSeatingAlg(date, session);
            return Ok(result);
        }

        [HttpPost("display/{date}/{session}")]
        public async Task<IActionResult> Display(string date, string session, [FromBody] List<RoomDictDto> halls)
        {
            Console.WriteLine(halls);
            foreach (var data in halls)
            {
                Console.WriteLine($"RegisterNumber:{data.RegisterNumber} Hall:{data.Hall} SeatNumber:{data.SeatNumber}");
            }

            Console.WriteLine($"Date :{date} Session :{session}");

            await _seatingCore.SetRooms(halls, date, session);

            return Ok(new { res = "success" });
        }

        [HttpDelete("exam/{date}/{session}")]
        public async Task<IActionResult> DeleteSeating(string date, string session)
        {
            try
            {
                await _seatingCore.DeleteSeating(date, session);
                return Ok(new { res = "Deleted Successfully" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error deleting seating" });
            }
        }

        [HttpGet("new")]
        public IActionResult Action1()
        {
            // _seatingCore.newSeatingAlg();
            return Ok(new { res = "success" });
        }

        [HttpGet("finalseating/{date}/{session}")]
        public async Task<IActionResult> FinalSeatingArrangement(string date, string session)
        {
            var res = await _seatingCore.GetFinalSeatingArrangement(date, session);
            return Ok(res);
        }

        [HttpGet("final/{date}/{session}")]
        public async Task<IActionResult> Action3(string date, string session)
        {
            await _seatingCore.TestRegNumberMethod(date, session);
            return Ok(new { res = "success" });
        }

        [HttpGet("consolidated/{date}/{session}")]
        public async Task<IActionResult> ConsolidatedReport(string date, string session)
        {
            var res = await _seatingCore.getConsolidatedReport(date, session);
            return Ok(res);
        }

        [HttpGet("noticeboard/{date}/{session}")]
        public async Task<IEnumerable> NoticeBoardResult(string date, string session)
        {
            var res = await _noticeBoardService.GetRegisterNumberForNotice(date, session);
            return res;
        }

        [HttpPut("updateexamdetails")]
        public async Task<IActionResult> UpdateExam(ExamDto examDto)
        {
            try
            {
                await _seatingCore.UpdateExamData(examDto);
                return Ok(new { res = "Success !!!" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error Occured" });
            }
        }

        [HttpPut("updateexamsdetails")]
        public async Task<IActionResult> UpdateExams(MinExamDto minExamDto)
        {
            try
            {
                await _seatingRepository.UpdateExamDate(minExamDto);
                return Ok(new { res = "Success !!!" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error Occured" });
            }
        }

        [HttpPost("room/{roomName}")]
        public async Task<IActionResult> CreateRoom(string roomName)
        {
            await _seatingRepository.CreateRoom(roomName);
            return Ok(new { res = "Success !!!" });
        }

        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
        {
            var result = await _seatingRepository.GetRooms();
            return Ok(result);
        }

        [HttpGet("rooms/{date}/{session}")]
        public async Task<IActionResult> GetRoomsByDateAndSession(string date, string session)
        {
            var res = await _seatingRepository.GetRoomsDateAndSessionWise(date, session);
            return Ok(res);
        }

        [HttpPut("room")]
        public async Task<IActionResult> UpdateRooms(MinRoomDetailsDto minRoomDetailsDto)
        {
            try
            {
                await _seatingRepository.UpdateRoom(minRoomDetailsDto);
                return Ok(new { res = "Success !!!" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error Occured" });
            }
        }

        [HttpDelete("room/{roomName}")]
        public async Task<IActionResult> DeleteRoom(string roomName)
        {
            try
            {
                await _seatingRepository.DeleteRoom(roomName);
                return Ok(new { res = "Success !!!" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error Occured" });
            }
        }

        [AllowAnonymous]
        [HttpGet("hallticket/{registerNumber}")]
        public async Task<IActionResult> GetHallTicket(string registerNumber)
        {
            try
            {
                var res = await _seatingCore.GetHallTicket(registerNumber);
                return Ok(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new { res = "Error Occured" });
            }
        }
    }
}