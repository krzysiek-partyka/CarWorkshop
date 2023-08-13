using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private readonly ICarWorkshopRepository _carWorkshopRepository;
        private readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(ICarWorkshopRepository carWorkshopRepository, IUserContext userContext)        {
            _carWorkshopRepository = carWorkshopRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            //if(currentUser == null && !currentUser.IsInRole("Moderator"))
            //{
            //    return Unit.Value;
            //}

            var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.EncodedName!);

            
            var isEditable = currentUser != null && (currentUser.Id == carWorkshop.CreatedById || currentUser.IsInRole("Moderator"));

            if(!isEditable)
            {
                return Unit.Value;
            }
            carWorkshop.Description = request.Description;
            carWorkshop.ContactDetails.City = request.City;
            carWorkshop.ContactDetails.Street = request.Street;
            carWorkshop.ContactDetails.PostalCode = request.PostalCode;
            carWorkshop.ContactDetails.PhoneNumber = request.PhoneNumber;
            carWorkshop.EncodeName();
            await _carWorkshopRepository.Commit();
            return Unit.Value;
        }
    }
}
