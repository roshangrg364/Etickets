using CoreModule.Source.Dto.Producer;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Repository;
using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public class ProducerService : ProducerServiceInterface
    {

        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly FilerHelperInterface _fileHelper;
        public ProducerService(UnitOfWorkInterface unitOfWork,
            FilerHelperInterface filerHelper)
        {
            _unitOfWork = unitOfWork;
            _fileHelper = filerHelper;
        }
       
        public async Task Create(ProducerCreateDto dto)
        {
            var producer = new Producer(dto.Name, dto.Description);
            if (!string.IsNullOrWhiteSpace(dto.Image))
            {
                dto.Image = _fileHelper.SaveImageAndGetFileName(dto.Image);
                producer.SetImage(dto.Image);
            }
            await _unitOfWork.Producers.AddAsync(producer).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }

        public async Task Update(ProducerUpdateDto dto)
        {
            var producer = await _unitOfWork.Producers.GetByIdAsync(dto.Id).ConfigureAwait(false) ?? throw new ProducerNotFoundException();
            if (!string.IsNullOrWhiteSpace(dto.Image))
            {
                if (!string.IsNullOrWhiteSpace(producer.Image))
                {
                    _fileHelper.RemoveFile(producer.Image);
                }
                dto.Image = _fileHelper.SaveImageAndGetFileName(dto.Image);
                producer.SetImage(dto.Image);

            }
            producer.Update(dto.Name, dto.Description);
            await _unitOfWork.Producers.Update(producer).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }
    }
}
