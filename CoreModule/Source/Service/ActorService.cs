using CoreModule.Source.Dto.Actor;
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
    public class ActorService : ActorServiceInterface
    {
        private readonly FilerHelperInterface _filerHelper;
        private readonly UnitOfWorkInterface _unitOfWork;
        public ActorService(FilerHelperInterface filerHelper,
            UnitOfWorkInterface unitOfWork)
        {
            _filerHelper = filerHelper;
            _unitOfWork = unitOfWork;
        }
        public async Task Create(ActorCreateDto dto)
        {
           
            var actor = new Actor(dto.Name, dto.Description);
            if (!string.IsNullOrWhiteSpace(dto.Image))
            {
                dto.Image = _filerHelper.SaveImageAndGetFileName(dto.Image);
                actor.SetImage(dto.Image);
            }
            await _unitOfWork.Actors.AddAsync(actor).ConfigureAwait(false);
            await _unitOfWork.Complete();

        }

        public async Task Remove(int id)
        {
            var actor = await _unitOfWork.Actors.GetByIdAsync(id).ConfigureAwait(false) ?? throw new ActorNotFoundException();
            await _unitOfWork.Actors.Remove(actor).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }

        public async Task Update(ActorUpdateDto dto)
        {
            var actor = await _unitOfWork.Actors.GetByIdAsync(dto.Id).ConfigureAwait(false) ?? throw new ActorNotFoundException();
            if(!string.IsNullOrWhiteSpace(dto.Image))
            {
                if(!string.IsNullOrWhiteSpace(actor.Image))
                {
                    _filerHelper.RemoveFile(actor.Image); 
                }
                dto.Image = _filerHelper.SaveImageAndGetFileName(dto.Image);
                actor.SetImage(dto.Image);

            }
            actor.Update(dto.Name, dto.Description);
            await _unitOfWork.Actors.Update(actor).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }
    }
}
