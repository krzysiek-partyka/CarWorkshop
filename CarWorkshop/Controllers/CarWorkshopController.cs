using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarworkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkshop.MVC.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(EditCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var carWorkshop = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));

            if(!carWorkshop.IsEditable)
            {
                return RedirectToAction("NoAccess","Home");
            }

            EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(carWorkshop);

            return View(model);
        }

        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var carWorkshop = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
            return View(carWorkshop);
        }
        [HttpGet]
        [Authorize(Roles = "Owner, Moderator")]
        public async Task<IActionResult> Index()
        {
            var  carWorkshop =  await _mediator.Send(new GetAllCarworkshopsQuery());
            return View(carWorkshop);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
         {
            if(!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

    }
}
