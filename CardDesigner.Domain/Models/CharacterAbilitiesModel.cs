using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
using System.Linq;

namespace CardDesigner.Domain.Models
{
    public class CharacterAbilitiesModel
    {
        // Entity links
        public int ID { get; set; }
        public CharacterModel Character { get; set; }

        // Properties

        #region Abilities

        public int Proficiency { get; set; }

        public bool StrengthSavingThrows { get; set; }
        public int StrengthLevel { get; set; } = 8;
        public bool DexteritySavingThrows { get; set; }
        public int DexterityLevel { get; set; } = 8;
        public bool ConstitutionSavingThrows { get; set; }
        public int ConstitutionLevel { get; set; } = 8;
        public bool InteligenceSavingThrows { get; set; }
        public int InteligenceLevel { get; set; } = 8;
        public bool WisdomSavingThrows { get; set; }
        public int WisdomLevel { get; set; } = 8;
        public bool CharismaSavingThrows { get; set; }
        public int CharismaLevel { get; set; } = 8;

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

        public AbilityModel Strength { get => GetAbility(Ability.Strength); set => SetAbility(value); }
        public AbilityModel Dexterity { get => GetAbility(Ability.Dexterity); set => SetAbility(value); }
        public AbilityModel Constitution { get => GetAbility(Ability.Constitution); set => SetAbility(value); }
        public AbilityModel Inteligence { get => GetAbility(Ability.Inteligence); set => SetAbility(value); }
        public AbilityModel Wisdom { get => GetAbility(Ability.Wisdom); set => SetAbility(value); }
        public AbilityModel Charisma { get => GetAbility(Ability.Charisma); set => SetAbility(value); }

        private void SetBonuses(AbilityModel attributeModel)
        {
            attributeModel.LevelBonus = (attributeModel.Level - 10) / 2;
            attributeModel.SavingThrowsBonus = attributeModel.HasSavingThrows ? attributeModel.LevelBonus + Proficiency : attributeModel.LevelBonus;

            if (attributeModel.Skills.Any())
            {
                foreach (SkillModel skillModel in attributeModel.Skills)
                {
                    if (skillModel.IsProficient)
                    {
                        skillModel.Bonus = Proficiency + attributeModel.LevelBonus;
                    }
                    else if (skillModel.IsExpert)
                    {
                        skillModel.Bonus = 2 * Proficiency + attributeModel.LevelBonus;
                    }
                    else if (skillModel.IsBasic)
                    {
                        skillModel.Bonus = attributeModel.LevelBonus;
                    }
                }
            }
        }

        /// <summary>
        /// Getter method to get all values for selected skill
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public AbilityModel GetAbility(Ability attribute)
        {
            AbilityModel attributeModel = new()
            {
                Skills = new(),
                Type = attribute
            };
            switch (attribute)
            {
                case Ability.Strength:
                    attributeModel.HasSavingThrows = StrengthSavingThrows;
                    attributeModel.Level = StrengthLevel;
                    attributeModel.Skills = new()
                    {
                        new SkillModel()
                        {
                            Type = Skill.Athletics,
                            IsBasic = !(AthleticsExperties || AthleticsProficiency),
                            IsExpert = AthleticsExperties,
                            IsProficient = AthleticsProficiency,
                        }
                    };
                    break;
                case Ability.Dexterity:
                    attributeModel.HasSavingThrows = DexteritySavingThrows;
                    attributeModel.Level = DexterityLevel;
                    attributeModel.Skills = new()
                    {
                        new SkillModel()
                        {
                            Type = Skill.Acrobatics,
                            IsBasic = !(AcrobaticsExperties || AcrobaticsProficiency),
                            IsExpert = AcrobaticsExperties,
                            IsProficient = AcrobaticsProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.SleightOfHand,
                            IsBasic = !(SleightOfHandExperties || SleightOfHandProficiency),
                            IsExpert = SleightOfHandExperties,
                            IsProficient = SleightOfHandProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Stealth,
                            IsBasic = !(StealthExperties || StealthProficiency),
                            IsExpert = StealthExperties,
                            IsProficient = StealthProficiency,
                        }
                    };
                    break;
                case Ability.Constitution:
                    attributeModel.HasSavingThrows = ConstitutionSavingThrows;
                    attributeModel.Level = ConstitutionLevel;
                    break;
                case Ability.Inteligence:
                    attributeModel.HasSavingThrows = InteligenceSavingThrows;
                    attributeModel.Level = InteligenceLevel;
                    attributeModel.Skills = new()
                    {
                        new SkillModel()
                        {
                            Type = Skill.Arcana,
                            IsBasic = !(ArcanaExperties || ArcanaProficiency),
                            IsExpert = ArcanaExperties,
                            IsProficient = ArcanaProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.History,
                            IsBasic = !(HistoryExperties || HistoryProficiency),
                            IsExpert = HistoryExperties,
                            IsProficient = HistoryProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Investigation,
                            IsBasic = !(InvestigationExperties || InvestigationProficiency),
                            IsExpert = InvestigationExperties,
                            IsProficient = InvestigationProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Nature,
                            IsBasic = !(NatureExperties ||NatureProficiency),
                            IsExpert = NatureExperties,
                            IsProficient = NatureProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Religion,
                            IsBasic = !(ReligionExperties || ReligionProficiency),
                            IsExpert = ReligionExperties,
                            IsProficient = ReligionProficiency,
                        }
                    };
                    break;
                case Ability.Wisdom:
                    attributeModel.HasSavingThrows = WisdomSavingThrows;
                    attributeModel.Level = WisdomLevel;
                    attributeModel.Skills = new()
                    {
                        new SkillModel()
                        {
                            Type = Skill.AnimalHandling,
                            IsBasic = !(AnimalHandlingExperties || AnimalHandlingProficiency),
                            IsExpert = AnimalHandlingExperties,
                            IsProficient = AnimalHandlingProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Insight,
                            IsBasic = !(InsightExperties || InsightProficiency),
                            IsExpert = InsightExperties,
                            IsProficient = InsightProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Medicine,
                            IsBasic = !(MedicineExperties || MedicineProficiency),
                            IsExpert = MedicineExperties,
                            IsProficient = MedicineProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Perception,
                            IsBasic = !(PerceptionExperties || PerceptionProficiency),
                            IsExpert = PerceptionExperties,
                            IsProficient = PerceptionProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Survival,
                            IsBasic = !(SurvivalExperties || SurvivalProficiency),
                            IsExpert = SurvivalExperties,
                            IsProficient = SurvivalProficiency,
                        }
                    };
                    break;
                case Ability.Charisma:
                    attributeModel.HasSavingThrows = CharismaSavingThrows;
                    attributeModel.Level = CharismaLevel;
                    attributeModel.Skills = new()
                    {
                        new SkillModel()
                        {
                            Type = Skill.Deception,
                            IsBasic = !(DeceptionExperties || DeceptionProficiency),
                            IsExpert = DeceptionExperties,
                            IsProficient = DeceptionProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Intimidation,
                            IsBasic = !(IntimidationExperties || IntimidationProficiency),
                            IsExpert = IntimidationExperties,
                            IsProficient = IntimidationProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Performance,
                            IsBasic = !(PerformanceExperties || PerformanceProficiency),
                            IsExpert = PerformanceExperties,
                            IsProficient = PerformanceProficiency,
                        },
                        new SkillModel()
                        {
                            Type = Skill.Persuasion,
                            IsBasic = !(PersuasionExperties || PersuasionProficiency),
                            IsExpert = PersuasionExperties,
                            IsProficient = PersuasionProficiency,
                        }
                    };
                    break;
                default:
                    break;
            }

            SetBonuses(attributeModel);

            return attributeModel;
        }

        /// <summary>
        /// Setter method for attributes
        /// </summary>
        /// <param name="attributeModel"></param>
        public void SetAbility(AbilityModel attributeModel)
        {
            switch (attributeModel.Type)
            {
                case Ability.Strength:
                    StrengthLevel = attributeModel.Level;
                    StrengthSavingThrows = attributeModel.HasSavingThrows;
                    break;
                case Ability.Dexterity:
                    DexterityLevel = attributeModel.Level;
                    DexteritySavingThrows = attributeModel.HasSavingThrows;
                    break;
                case Ability.Constitution:
                    ConstitutionLevel = attributeModel.Level;
                    ConstitutionSavingThrows = attributeModel.HasSavingThrows;
                    break;
                case Ability.Inteligence:
                    InteligenceLevel = attributeModel.Level;
                    InteligenceSavingThrows = attributeModel.HasSavingThrows;
                    break;
                case Ability.Wisdom:
                    WisdomLevel = attributeModel.Level;
                    WisdomSavingThrows = attributeModel.HasSavingThrows;
                    break;
                case Ability.Charisma:
                    CharismaLevel = attributeModel.Level;
                    CharismaSavingThrows = attributeModel.HasSavingThrows;
                    break;
                default:
                    break;
            }

            foreach (SkillModel skill in attributeModel.Skills)
            {
                SetSkill(skill);
            }
        }

        /// <summary>
        /// Setter method for skill properties
        /// </summary>
        /// <param name="skillModel"></param>
        private void SetSkill(SkillModel skillModel)
        {
            switch (skillModel.Type)
            {
                case Skill.Athletics:
                    AthleticsExperties = skillModel.IsExpert;
                    AthleticsProficiency = skillModel.IsProficient;
                    break;
                case Skill.Acrobatics:
                    AcrobaticsExperties = skillModel.IsExpert;
                    AcrobaticsProficiency = skillModel.IsProficient;
                    break;
                case Skill.SleightOfHand:
                    SleightOfHandExperties = skillModel.IsExpert;
                    SleightOfHandProficiency = skillModel.IsProficient;
                    break;
                case Skill.Stealth:
                    StealthExperties = skillModel.IsExpert;
                    StealthProficiency = skillModel.IsProficient;
                    break;
                case Skill.Arcana:
                    ArcanaExperties = skillModel.IsExpert;
                    ArcanaProficiency = skillModel.IsProficient;
                    break;
                case Skill.History:
                    HistoryExperties = skillModel.IsExpert;
                    HistoryProficiency = skillModel.IsProficient;
                    break;
                case Skill.Investigation:
                    InsightExperties = skillModel.IsExpert;
                    InvestigationProficiency = skillModel.IsProficient;
                    break;
                case Skill.Nature:
                    NatureExperties = skillModel.IsExpert;
                    NatureProficiency = skillModel.IsProficient;
                    break;
                case Skill.Religion:
                    ReligionExperties = skillModel.IsExpert;
                    ReligionProficiency = skillModel.IsProficient;
                    break;
                case Skill.AnimalHandling:
                    AnimalHandlingExperties = skillModel.IsExpert;
                    AnimalHandlingProficiency = skillModel.IsProficient;
                    break;
                case Skill.Insight:
                    InsightExperties = skillModel.IsExpert;
                    InsightProficiency = skillModel.IsProficient;
                    break;
                case Skill.Medicine:
                    MedicineExperties = skillModel.IsExpert;
                    MedicineProficiency = skillModel.IsProficient;
                    break;
                case Skill.Perception:
                    PerceptionExperties = skillModel.IsExpert;
                    PerceptionProficiency = skillModel.IsProficient;
                    break;
                case Skill.Survival:
                    SurvivalExperties = skillModel.IsExpert;
                    SurvivalProficiency = skillModel.IsProficient;
                    break;
                case Skill.Deception:
                    DeceptionExperties = skillModel.IsExpert;
                    DeceptionProficiency = skillModel.IsProficient;
                    break;
                case Skill.Intimidation:
                    IntimidationExperties = skillModel.IsExpert;
                    IntimidationProficiency = skillModel.IsProficient;
                    break;
                case Skill.Performance:
                    PerformanceExperties = skillModel.IsExpert;
                    PerformanceProficiency = skillModel.IsProficient;
                    break;
                case Skill.Persuasion:
                    PersuasionExperties = skillModel.IsExpert;
                    PersuasionProficiency = skillModel.IsProficient;
                    break;
            }
        }

    }
}
