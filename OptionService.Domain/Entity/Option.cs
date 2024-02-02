using DomainCommons.Models;
using OptionService.Domain.Event;


namespace OptionService.Domain.Entity
{
    public record class Option : AggregateRootEntity, IAggregateRoot, IHasCreationTime, IHasModificationTime
    {
        public enum OptionType
        {
            Public = 0,
            User = 1
        }

        private Option() { }

        public string Name { get; private set; }
        public string Value { get; private set; }
        
        /// <summary>
        /// 可能是组，也可能是用户GUID，用于描述它们的可见性
        /// 当该字段为空的时候为所有可见
        /// </summary>
        public Guid? OwnerId { get; private set; }

        /// <summary>
        /// 决定GUID的类型
        /// </summary>
        public int Type;

        public override void SoftDelete()
        {
            base.SoftDelete();
            this.AddDomainEventIfAbsent(new OptionDeletedEvent(this.Id));
        }

        public void Edit(string value)
        {
            this.Value = value;
            this.NotifyUpdated();
            this.AddDomainEvent(new OptionUpdatedEvent(this));
        }
        


        public class Builder
        {
            private string _name;
            private string _value;
            private Guid? _ownerId;
            private int _type;

            public Builder() { }

            public Builder Name(string value)
            {
                this._name = value;
                return this;
            }

            public Builder Value(string value)
            {
                this._value = value;
                return this;
            }

            public Builder OwnerId(Guid? value)
            {
                this._ownerId = value;
                return this;
            }

            public Builder Type(int value)
            {
                this._type = value;
                return this;
            }

            public Option Build()
            {
                Option option = new Option();
                option.Name = _name;
                option.Value = _value;
                option.OwnerId = _ownerId;
                option.Type = _type;
                option.AddDomainEvent(new OptionCreatedEvent(option));
                return option;
            }

        }

    }
}
