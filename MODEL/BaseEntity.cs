using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace MODEL
{
    public enum EntityStatus { ADDED, MODIFIED, DELETED, UNCHANGED }

    public abstract class BaseEntity
    {
        private int id;
        private EntityStatus entityStatus;

        protected BaseEntity() : this(EntityStatus.UNCHANGED) { }

        protected BaseEntity(EntityStatus entityStatus)
        {
            this.entityStatus = entityStatus;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get => id; set => id = value; }

        [Ignore]
        public EntityStatus EntityStatus { get => entityStatus; set => entityStatus = value; }

        public override bool Equals(object obj)
        {
            return obj is BaseEntity entity &&
                   id == entity.id &&
                   entityStatus == entity.entityStatus;
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            return EqualityComparer<BaseEntity>.Default.Equals(left, right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }
    }
}
