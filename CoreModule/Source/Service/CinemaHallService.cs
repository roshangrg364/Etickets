using CoreModule.Source.Dto.CinemalHall;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public class CinemaHallService : CinemaHallServiceInterface
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly FilerHelperInterface _fileHelper;
        public CinemaHallService(UnitOfWorkInterface unitOfWork,
            FilerHelperInterface fileHelper)
        {
            _unitOfWork = unitOfWork;
            _fileHelper = fileHelper;
        }
        public async Task Create(CinemaHallCreatDto dto)
        {
            var cinema = new CinemaHall(dto.Name, dto.Description);
            if (!string.IsNullOrWhiteSpace(dto.Image))
            {
                dto.Image = _fileHelper.SaveImageAndGetFileName(dto.Image);
                cinema.SetImage(dto.Image);
            }
            await _unitOfWork.CinemaHalls.AddAsync(cinema).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }

        public async Task Update(CinemaHallUpdateDto dto)
        {
            var cinema = await _unitOfWork.CinemaHalls.GetByIdAsync(dto.Id).ConfigureAwait(false) ?? throw new CinemaNotFoundException();
            if (!string.IsNullOrWhiteSpace(dto.Image))
            {
                if (!string.IsNullOrWhiteSpace(cinema.Image))
                {
                    _fileHelper.RemoveFile(cinema.Image);
                }
                dto.Image = _fileHelper.SaveImageAndGetFileName(dto.Image);
                cinema.SetImage(dto.Image);

            }
            cinema.Update(dto.Name, dto.Description);
            await _unitOfWork.CinemaHalls.Update(cinema).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }
    }
}
