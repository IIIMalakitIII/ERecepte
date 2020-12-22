using AutoMapper;
using EReceipt.DAL.Entities;
using EReceipt.ViewModels;

namespace EReceipt.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapViewModels();
        }

        private void MapViewModels()
        {
            CreateMap<DoctorViewModel, Doctor>().ReverseMap();
            CreateMap<ConfidantViewModel, Confidant>().ReverseMap();
            CreateMap<DiseaseHistoryViewModel, DiseaseHistory>().ReverseMap();
            CreateMap<ManufacturerViewModel, Manufacturer>().ReverseMap();
            CreateMap<MedicalInstitutionViewModel, MedicalInstitution>().ReverseMap();
            CreateMap<MedicamentCategoryViewModel, MedicamentCategory>().ReverseMap();
            CreateMap<MedicamentViewModel, Medicament>()
                .ForMember(d => d.Picture,
                    m => m.Ignore())
                .ForMember(d => d.ContentType,
                    m => m.Ignore());
            CreateMap<Medicament, MedicamentViewModel>()
                .ForMember(d => d.Picture,
                    m => m.Ignore())
                .ForMember(d => d.PictureByte,
                    m => m.MapFrom(s => s.Picture))
                .ForMember(d => d.ManufacturerName,
                    m => m.MapFrom(s => s.Manufacturer.Name))
                .ForMember(d => d.ManufacturerLicense,
                    m => m.MapFrom(s => s.Manufacturer.License));
            CreateMap<PatientViewModel, Patient>().ReverseMap();
            CreateMap<PharmacyViewModel, Pharmacy>().ReverseMap();
            CreateMap<RecordViewModel, Record>();
            CreateMap<Record, RecordViewModel>()
                .ForMember(d => d.MedicalInstitution,
                    m => m.MapFrom(s => 
                        $"{s.Doctor.MedicalInstitution.Name}, {s.Doctor.MedicalInstitution.Country}, {s.Doctor.MedicalInstitution.City}, {s.Doctor.MedicalInstitution.Address}"));

            CreateMap<ReceiptViewModel, Receipt>()
                .ForMember(d => d.Doctor,
                    m => m.Ignore())
                .ForMember(d => d.Medicament,
                    m => m.Ignore())
                .ForMember(d => d.Patient,
                    m => m.Ignore());

            CreateMap<Receipt, ReceiptViewModel>()
                .ForMember(d => d.MedicamentCategory,
                    m => m.MapFrom(s => s.Medicament.MedicamentCategory))
                .ForMember(d => d.MedicalInstitution,
                    m => m.MapFrom(s =>
                        $"{s.Doctor.MedicalInstitution.Name}, {s.Doctor.MedicalInstitution.Country}, {s.Doctor.MedicalInstitution.City}, {s.Doctor.MedicalInstitution.Address}"));

            CreateMap<MedicamentCategory, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => s.Name));
            CreateMap<MedicalInstitution, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => $"{s.Name}, {s.Country}, {s.City}, {s.Address}"));
            CreateMap<Manufacturer, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => s.Name));
            CreateMap<DiseaseHistory, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => s.Description));
            CreateMap<Confidant, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => $"{s.FirstName} {s.LastName}"));
            CreateMap<Doctor, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => $"{s.FirstName} {s.LastName}"));
            CreateMap<Medicament, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => s.Name));
            CreateMap<Patient, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => $"{s.FirstName} {s.LastName}"));
            CreateMap<Pharmacy, SelectViewModel>()
                .ForMember(d => d.Name,
                    m => m.MapFrom(s => $"{s.Country}, {s.City}, {s.Address}"));
        }
    }
}
