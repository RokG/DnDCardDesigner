﻿using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellDeckCreator
    {
        Task<SpellDeckModel> CreateSpellDeck(SpellDeckModel spellDeck);
    }
}
