﻿namespace RoleplayReady.Domain.Rules.DnD5e;

// ReSharper disable once InconsistentNaming - Dnd5e is the official name of the system.
public static partial class DnD5eDefinitions
{
    public static void AddEntityTypeModifiers(this DnD5e system)
    {
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = "SetEntity",
            IsApplicable = _ => true,
            Apply = e =>
            {
                e.Properties.Add(CreateElementProperty<string>(e, "Size"));
                e.Properties.Add(CreateElementProperty<int>(e, "Strength"));
                e.Properties.Add(CreateElementProperty<int>(e, "Dexterity"));
                e.Properties.Add(CreateElementProperty<int>(e, "Constitution"));
                e.Properties.Add(CreateElementProperty<int>(e, "Intelligence"));
                e.Properties.Add(CreateElementProperty<int>(e, "Wisdom"));
                e.Properties.Add(CreateElementProperty<int>(e, "Charisma"));
                e.Properties.Add(CreateElementProperty<int>(e, "ProficiencyBonus"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "SavingThrowProficiencies"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "SkillProficiencies"));
                e.Properties.Add(CreateElementProperty<string>(e, "PassivePerception"));
                e.Properties.Add(CreateElementProperty<string>(e, "PassiveInsight"));
                e.Properties.Add(CreateElementProperty<int>(e, "MaximumHitPoints"));
                e.Properties.Add(CreateElementProperty<int>(e, "CurrentHitPoints"));
                e.Properties.Add(CreateElementProperty<int>(e, "TemporaryHitPoints"));
                e.Properties.Add(CreateElementProperty<int>(e, "ArmorClass"));
                e.Properties.Add(CreateElementProperty<Dictionary<string, int>>(e, "Moviments"));
                e.Properties.Add(CreateElementProperty<string>(e, "Alignment"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Languages"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Senses"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Resistances"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Immunities"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Vulnerabilities"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Traits"));
                return e;
            },
            Validations = new List<ElementValidation>
            {
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Strength", strength => strength is < 0 or > 30, "Invalid strength."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Dexterity", strength => strength is < 0 or > 30, "Invalid dexterity."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Constitution", strength => strength is < 0 or > 30, "Invalid constitution."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Intelligence", strength => strength is < 0 or > 30, "Invalid intelligence."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Wisdom", strength => strength is < 0 or > 30, "Invalid wisdom."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Charisma", strength => strength is < 0 or > 30, "Invalid charisma."),
                CreatePropertyValidation<Dictionary<string, int>>(ValidationSeverityLevel.Error, "Moviments", moviments => (moviments ?? new Dictionary<string, int>()).All(move => move.Value >= 0), "Invalid speed."),
                CreatePropertyValidation<string[]>(ValidationSeverityLevel.Error, "SavingThrowProficiencies", savingThrows => {
                    var allowedAbilities = new[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
                    return savingThrows is null ||
                           savingThrows.Length == 0 ||
                           savingThrows.All(x => allowedAbilities.Contains(x));
                }, "At least one of the listed saving throw proficiencies is invalid."),
                CreatePropertyValidation<string[]>(ValidationSeverityLevel.Error, "SkillProficiencies", skills => {
                    var allowedSkillProficiencies = new[]
                    {
                        "Acrobatics", "Animal Handling", "Arcana", "Athletics", "Deception", "History", "Insight", "Intimidation", "Investigation",
                        "Medicine", "Nature", "Perception", "Performance", "Persuasion", "Religion", "Sleight of Hand", "Stealth", "Survival"
                    };
                    return skills is null ||
                           skills.Length == 0 ||
                           skills.All(x => allowedSkillProficiencies.Contains(x));
                }, "At least one of the skill throw proficiencies is invalid."),
                CreatePropertyValidation<string>(ValidationSeverityLevel.Error, "Size", size =>
                {
                    var allowedSizes = new[] { "Tiny", "Small", "Medium", "Large", "Huge", "Gargantuan" };
                    return !string.IsNullOrWhiteSpace(size) && allowedSizes.Contains(size);
                }, "Invalid size."),
                CreatePropertyValidation<string>(ValidationSeverityLevel.Error, "Alignment", alignment =>
                {
                    var allowedAlignments = new[]
                    {
                        "Lawful Good", "Neutral Good", "Chaotic Good",
                        "Lawful Neutral", "True Neutral", "Chaotic Neutral",
                        "Lawful Evil", "Neutral Evil", "Chaotic Evil",
                        "Unaligned"
                    };
                    return !string.IsNullOrWhiteSpace(alignment) && allowedAlignments.Contains(alignment);
                }, "Invalid alignment."),
            }
        });
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = $"SetCreature",
            IsApplicable = e => e.Type.Name == "Creature",
            Apply = e =>
            {
                e.Properties.Add(CreateElementProperty<string>(e, "CreatureType"));
                e.Properties.Add(CreateElementProperty<int>(e, "ChallengeRating"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Actions"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "LegendaryActions"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "LairActions"));
                return e;
            },
            Validations = new List<ElementValidation>
            {
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "ChallengeRating", cr => cr is < 0, "Invalid challenge rating."),
            }
        });
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = $"SetNPC",
            IsApplicable = e => e.Type.Name == "NonPlayerCharacter",
            Apply = e =>
            {
                e.Properties.Add(CreateElementProperty<string>(e, "NPCType"));
                e.Properties.Add(CreateElementProperty<string>(e, "Race"));
                e.Properties.Add(CreateElementProperty<Class[]>(e, "Classes"));
                e.Properties.Add(CreateElementProperty<string>(e, "Background"));
                e.Properties.Add(CreateElementProperty<int>(e, "ChallengeRating"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Actions"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "LegendaryActions"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "LairActions"));
                return e;
            },
            Validations = new List<ElementValidation>
            {
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "ChallengeRating", cr => cr is < 0, "Invalid challenge rating."),
                CreatePropertyValidation<Class[]>(ValidationSeverityLevel.Error, "Classes", classes => (classes ?? Array.Empty<Class>()).All(@class => @class.Level > 0), "Invalid speed."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Level", cr => cr is < 0, "Invalid level."),
            }
        });
        system.Modifiers.Add(new Modifier
        {
            System = system,
            Name = $"SetCharacter",
            IsApplicable = e => e.Type.Name == "Character",
            Apply = e =>
            {
                e.Properties.Add(CreateElementProperty<string>(e, "Race"));
                e.Properties.Add(CreateElementProperty<Class[]>(e, "Classes"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Archetypes"));
                e.Properties.Add(CreateElementProperty<string>(e, "Background"));
                e.Properties.Add(CreateElementProperty<Dictionary<string, int>>(e, "AbilityCharges"));
                e.Properties.Add(CreateElementProperty<string[]>(e, "Feats"));
                e.Properties.Add(CreateElementProperty<Dictionary<string, int>>(e, "Currency"));
                e.Properties.Add(CreateElementProperty<bool>(e, "Inspiration"));
                e.Properties.Add(CreateElementProperty<int>(e, "ExhaustionLevel"));
                e.Properties.Add(CreateElementProperty<int>(e, "CarryingCapacity"));
                e.Properties.Add(CreateElementProperty<int>(e, "CurrentCarryingWeight"));
                e.Properties.Add(CreateElementProperty<string>(e, "EyeColor"));
                e.Properties.Add(CreateElementProperty<string>(e, "Gender"));
                e.Properties.Add(CreateElementProperty<decimal>(e, "Height"));
                e.Properties.Add(CreateElementProperty<decimal>(e, "Weight"));
                e.Properties.Add(CreateElementProperty<string>(e, "HairColor"));
                e.Properties.Add(CreateElementProperty<string>(e, "SkinColor"));
                e.Properties.Add(CreateElementProperty<string>(e, "Appearance"));

                return e;
            },
            Validations = new List<ElementValidation>
            {
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "ChallengeRating", cr => cr is < 0, "Invalid challenge rating."),
                CreatePropertyValidation<int>(ValidationSeverityLevel.Error, "Level", cr => cr is < 0, "Invalid level."),
            }
        });
    }
}
