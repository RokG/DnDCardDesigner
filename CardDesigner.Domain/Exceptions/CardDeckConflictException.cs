using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Exceptions
{
    public class CardDeckConflictException : Exception
    {
        public ICard ExistingCard { get; }

        public ICard IncomingCard { get; }

        public CardDeckConflictException(ICard existingCard, ICard incomingCard) : base()
        {
            ExistingCard = existingCard;
            IncomingCard = incomingCard;
        }

        public CardDeckConflictException(string? message, ICard existingCard, ICard incomingCard) : base(message)
        {
            ExistingCard = existingCard;
            IncomingCard = incomingCard;
        }

        public CardDeckConflictException(string? message, Exception? innerException, ICard existingCard, ICard incomingCard) : base(message, innerException)
        {
            ExistingCard = existingCard;
            IncomingCard = incomingCard;
        }
    }
}