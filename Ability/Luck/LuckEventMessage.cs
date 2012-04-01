using System;

namespace Human80Level.Ability.Luck
{
    public class LuckEventMessage
    {
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool IsLuck { get; set; }

        public LuckEventMessage (string message, DateTime date, bool isLuck)
        {
            this.Message = message;
            this.Date = date;
            this.IsLuck = isLuck;
        }
    }
}
