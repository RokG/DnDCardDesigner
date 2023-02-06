using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterAttributesEntity
    {
        [Key]
        public int ID { get; set; }
        public CharacterEntity Character { get; set; }
        
        #region Attributes

        public int Proficiency { get; set; }
        
        public bool StrengthSavingThrows { get; set; }
        public int StrengthLevel { get; set; }
        public bool DexteritySavingThrows { get; set; }
        public int DexterityLevel { get; set; }
        public bool ConstitutionSavingThrows { get; set; }
        public int ConstitutionLevel { get; set; }
        public bool InteligenceSavingThrows { get; set; }
        public int InteligenceLevel { get; set; }
        public bool WisdomSavingThrows { get; set; }
        public int WisdomLevel { get; set; }
        public bool CharismaSavingThrows { get; set; }
        public int CharismaLevel { get; set; }

        #endregion

        #region StrengthSkills

        public bool AthleticsProficiency { get; set; }
        public bool AthleticsExperties { get; set; }

        #endregion

        #region DexteritySkills

        public bool AcrobaticsProficiency { get; set; }
        public bool AcrobaticsExperties { get; set; }
        public bool SleightOfHandProficiency { get; set; }
        public bool SleightOfHandExperties { get; set; }
        public bool StealthProficiency { get; set; }
        public bool StealthExperties { get; set; }

        #endregion

        #region InteligenceSkills

        public bool ArcanaProficiency { get; set; }
        public bool ArcanaExperties { get; set; }
        public bool HistoryProficiency { get; set; }
        public bool HistoryExperties { get; set; }
        public bool InvestigationProficiency { get; set; }
        public bool InvestigationExperties { get; set; }
        public bool NatureProficiency { get; set; }
        public bool NatureExperties { get; set; }
        public bool ReligionProficiency { get; set; }
        public bool ReligionExperties { get; set; }

        #endregion

        #region WisdomSkills

        public bool AnimalHandlingProficiency { get; set; }
        public bool AnimalHandlingExperties { get; set; }
        public bool InsightProficiency { get; set; }
        public bool InsightExperties { get; set; }
        public bool MedicineProficiency { get; set; }
        public bool MedicineExperties { get; set; }
        public bool PerceptionProficiency { get; set; }
        public bool PerceptionExperties { get; set; }
        public bool SurvivalProficiency { get; set; }
        public bool SurvivalExperties { get; set; }

        #endregion

        #region CarismaSkills

        public bool DeceptionProficiency { get; set; }
        public bool DeceptionExperties { get; set; }
        public bool IntimidationProficiency { get; set; }
        public bool IntimidationExperties { get; set; }
        public bool PerformanceProficiency { get; set; }
        public bool PerformanceExperties { get; set; }
        public bool PersuasionProficiency { get; set; }
        public bool PersuasionExperties { get; set; }

        #endregion
    }
}
