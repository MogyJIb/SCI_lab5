namespace lab4.Models.Sorts
{
    public class ClientSort
    {
        public Model Models { get; set; }

        public ClientSort()
        {
            Models = new Model(State.NoSort);
        }

        public ClientSort(State sortOrder)
        {
            Models = new Model(sortOrder);
        }

        public enum State
        {
            NoSort,

            NameAsc,
            NameDesc,

            PhoneAsc,
            PhoneDesc,

            BirthdayAsc,
            BirthdayDesc,

            AddressAsc,
            AddressDesc
        }

        public class Model
        {
            public State NameSort { get; set; } 
            public State PhoneSort { get; set; }
            public State BirthdaySort { get; set; }
            public State AddressSort { get; set; }

            public State CurrentState { get; set; }    

            public Model(State sortOrder)
            {
                NameSort = sortOrder == State.NameAsc ? State.NameDesc : State.NameAsc;
                PhoneSort = sortOrder == State.PhoneAsc ? State.PhoneDesc : State.PhoneAsc;
                BirthdaySort = sortOrder == State.BirthdayAsc ? State.BirthdayDesc : State.BirthdayAsc;
                AddressSort = sortOrder == State.AddressAsc ? State.AddressDesc : State.AddressAsc;

                CurrentState = sortOrder;
            }
        }
    }
}
