using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Stores
{
    public class CardDesignerStore
    {
        public SpellCard Card { get; set; }

        private readonly Lazy<Task> _initializeLazy;

        public CardDesignerStore()
        {
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        private async Task Initialize()
        {
            Card = new SpellCard();
            Card.Name = "My first card";
        }
    }
}