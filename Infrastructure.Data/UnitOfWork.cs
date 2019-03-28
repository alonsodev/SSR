using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IDisposable
    {
        #region Fields
        private readonly ApplicationDbContext _context;

        private UserRepository _userRepository;
        private PermissionRepository _permissionRepository;
        private RoleRepository _roleRepository;
        private RolePermissionRepository _rolePermissionRepository;

        private InvestigatorRepository _investigatorRepository;

        private InstitutionRepository _institutionRepository;
        private ProgramRepository _programRepository;
        private InvestigationGroupRepository _investigationGroupRepository;
        private InterestAreaRepository _interestAreaRepository;

        private CommissionRepository _commissionRepository;

        private DraftLawRepository _draftLawRepository;
        private ConceptRepository _conceptRepository;

        private BadLanguageRepository _badLanguageRepository;

        private TagRepository _tagRepository;
        private ConceptTagRepository _conceptTagRepository;
        private ConceptStatusLogRepository _conceptStatusLogRepository;

        private DebateSpeakerRepository _debateSpeakerRepository;

        private EducationalInstitutionRepository _educationalInstitutionRepository;
        private KnowledgeAreaRepository _knowledgeAreaRepository;
        private AcademicLevelRepository _academicLevelRepository;

        private EducationLevelRepository _educationLevelRepository;

        private SnieRepository _snieRepository;

        private MeritRangeRepository _meritRangeRepository;

        private InvestigatorInterestAreaRepository _investigatorInterestAreaRepository;
        private InvestigatorCommissionRepository _investigatorCommissionRepository;

        private ConfigurationRepository _configurationRepository;

        private NotificationRepository _notificationRepository;
        private ConceptDebateSpeakerRepository _conceptDebateSpeakerRepository;

        private ConsultationRepository _consultationRepository;
        private ConsultationInterestAreaRepository _consultationInterestAreaRepository;

        private DraftLawStatusRepository _draftLawStatusRepository;

        private PeriodRepository _periodRepository;
        private OriginRepository _originRepository;
        private UserInstitutionRepository _userInstitutionRepository;
        private ConsultationTypeRepository _consultationTypeRepository;

        

        #endregion

        #region Constructors
        public UnitOfWork(string nameOrConnectionString)
        {
            _context = new ApplicationDbContext(nameOrConnectionString);
        }
        #endregion

        #region IUnitOfWork Members

        public ConsultationTypeRepository ConsultationTypeRepository
        {
            get { return _consultationTypeRepository ?? (_consultationTypeRepository = new ConsultationTypeRepository(_context)); }

        }
        public UserInstitutionRepository UserInstitutionRepository
        {
            get { return _userInstitutionRepository ?? (_userInstitutionRepository = new UserInstitutionRepository(_context)); }

        }


        public OriginRepository OriginRepository
        {
            get { return _originRepository ?? (_originRepository = new OriginRepository(_context)); }

        }
        public PeriodRepository PeriodRepository
        {
            get { return _periodRepository ?? (_periodRepository = new PeriodRepository(_context)); }

        }
        public DraftLawStatusRepository DraftLawStatusRepository
        {
            get { return _draftLawStatusRepository ?? (_draftLawStatusRepository = new DraftLawStatusRepository(_context)); }

        }

        public ConsultationInterestAreaRepository ConsultationInterestAreaRepository
        {
            get { return _consultationInterestAreaRepository ?? (_consultationInterestAreaRepository = new ConsultationInterestAreaRepository(_context)); }

        }
        public ConsultationRepository ConsultationRepository
        {
            get { return _consultationRepository ?? (_consultationRepository = new ConsultationRepository(_context)); }

        }
        public ConceptDebateSpeakerRepository ConceptDebateSpeakerRepository
        {
            get { return _conceptDebateSpeakerRepository ?? (_conceptDebateSpeakerRepository = new ConceptDebateSpeakerRepository(_context)); }

        }
        public NotificationRepository NotificationRepository
        {
            get { return _notificationRepository ?? (_notificationRepository = new NotificationRepository(_context)); }

        }
        public ConfigurationRepository ConfigurationRepository
        {
            get { return _configurationRepository ?? (_configurationRepository = new ConfigurationRepository(_context)); }

        }
        public InvestigatorInterestAreaRepository InvestigatorInterestAreaRepository
        {
            get { return _investigatorInterestAreaRepository ?? (_investigatorInterestAreaRepository = new InvestigatorInterestAreaRepository(_context)); }

        }
        public InvestigatorCommissionRepository InvestigatorCommissionRepository
        {
            get { return _investigatorCommissionRepository ?? (_investigatorCommissionRepository = new InvestigatorCommissionRepository(_context)); }

        }
        public MeritRangeRepository MeritRangeRepository
        {
            get { return _meritRangeRepository ?? (_meritRangeRepository = new MeritRangeRepository(_context)); }

        }
        public SnieRepository SnieRepository
        {
            get { return _snieRepository ?? (_snieRepository = new SnieRepository(_context)); }

        }
        public EducationLevelRepository EducationLevelRepository
        {
            get { return _educationLevelRepository ?? (_educationLevelRepository = new EducationLevelRepository(_context)); }

        }
        public AcademicLevelRepository AcademicLevelRepository
        {
            get { return _academicLevelRepository ?? (_academicLevelRepository = new AcademicLevelRepository(_context)); }

        }
        public KnowledgeAreaRepository KnowledgeAreaRepository
        {
            get { return _knowledgeAreaRepository ?? (_knowledgeAreaRepository = new KnowledgeAreaRepository(_context)); }

        }
        public EducationalInstitutionRepository EducationalInstitutionRepository
        {
            get { return _educationalInstitutionRepository ?? (_educationalInstitutionRepository = new EducationalInstitutionRepository(_context)); }

        }
        public DebateSpeakerRepository DebateSpeakerRepository
        {
            get { return _debateSpeakerRepository ?? (_debateSpeakerRepository = new DebateSpeakerRepository(_context)); }

        }
        public ConceptStatusLogRepository ConceptStatusLogRepository
        {
            get { return _conceptStatusLogRepository ?? (_conceptStatusLogRepository = new ConceptStatusLogRepository(_context)); }

        }
        public ConceptTagRepository ConceptTagRepository
        {
            get { return _conceptTagRepository ?? (_conceptTagRepository = new ConceptTagRepository(_context)); }

        }
        public TagRepository TagRepository
        {
            get { return _tagRepository ?? (_tagRepository = new TagRepository(_context)); }

        }
        public BadLanguageRepository BadLanguageRepository
        {
            get { return _badLanguageRepository ?? (_badLanguageRepository = new BadLanguageRepository(_context)); }

        }
        public ConceptRepository ConceptRepository
        {
            get { return _conceptRepository ?? (_conceptRepository = new ConceptRepository(_context)); }

        }

        public DraftLawRepository DraftLawRepository
        {
            get { return _draftLawRepository ?? (_draftLawRepository = new DraftLawRepository(_context)); }

        }

        public CommissionRepository CommissionRepository
        {
            get { return _commissionRepository ?? (_commissionRepository = new CommissionRepository(_context)); }

        }
        public InstitutionRepository InstitutionRepository
        {
            get { return _institutionRepository ?? (_institutionRepository = new InstitutionRepository(_context)); }

        }

        public ProgramRepository ProgramRepository
        {
            get { return _programRepository ?? (_programRepository = new ProgramRepository(_context)); }

        }

        public InvestigationGroupRepository InvestigationGroupRepository
        {
            get { return _investigationGroupRepository ?? (_investigationGroupRepository = new InvestigationGroupRepository(_context)); }

        }

        public InvestigatorRepository InvestigatorRepository
        {
            get { return _investigatorRepository ?? (_investigatorRepository = new InvestigatorRepository(_context)); }

        }

        public InterestAreaRepository InterestAreaRepository
        {
            get { return _interestAreaRepository ?? (_interestAreaRepository = new InterestAreaRepository(_context)); }

        }

        public RolePermissionRepository RolePermissionRepository
        {
            get { return _rolePermissionRepository ?? (_rolePermissionRepository = new RolePermissionRepository(_context)); }
        }
        public UserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }
        public PermissionRepository PermissionRepository
        {
            get { return _permissionRepository ?? (_permissionRepository = new PermissionRepository(_context)); }
        }
        public RoleRepository RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_context)); }
        }

        public int SaveChanges()
        {
            int intResultado = 0;
            try
            {

                intResultado = _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return intResultado;
        }



        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            this._userRepository = null;
            this._permissionRepository = null;
            this._roleRepository = null;

            this._rolePermissionRepository = null;

            this._investigatorRepository = null;

            this._institutionRepository = null;
            this._interestAreaRepository = null;
            this._programRepository = null;
            this._investigationGroupRepository = null;

            this._commissionRepository = null;
            this._draftLawRepository = null;
            this._conceptRepository = null;
            this._badLanguageRepository = null;
            this._tagRepository = null;
            this._conceptStatusLogRepository = null;
            this._debateSpeakerRepository = null;
            this._educationalInstitutionRepository = null;
            this._knowledgeAreaRepository = null;
            this._academicLevelRepository = null;
            this._educationLevelRepository = null;
            this._snieRepository = null;
            this._meritRangeRepository = null;
            this._investigatorCommissionRepository = null;
            this._investigatorInterestAreaRepository = null;
            this._configurationRepository = null;
            this._notificationRepository = null;
            this._conceptDebateSpeakerRepository = null;
            this._consultationRepository = null;
            this._consultationInterestAreaRepository = null;
            this._draftLawStatusRepository = null;
            this._periodRepository = null;
            this._originRepository = null;
            this._userInstitutionRepository = null;
            this._consultationTypeRepository = null;
            _context.Dispose();
        }
        #endregion
    }
}
