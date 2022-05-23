using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellCardCreator
    {
        Task<SpellCardModel> CreateSpellCard(SpellCardModel spellCard);
    }
}