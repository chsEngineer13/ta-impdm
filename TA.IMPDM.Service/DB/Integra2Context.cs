using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TA.IMPDM.Service.DB
{
    public class Integra2Context : DbContext
    {
        public Integra2Context()
        {
        }

        public Integra2Context(DbContextOptions<Integra2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Constrpart> ConstrParts { get; set; }
        public virtual DbSet<Construction> Constructions { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Docset> Docsets { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<ObjRef> ObjRefs { get; set; }
        public virtual DbSet<Packet> Packets { get; set; }
        public virtual DbSet<PacketLog> PacketLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("building");

                entity.ForNpgsqlHasComment("Здание/Сооружение");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Код здания, сооружения (8-й атрибут обозначения)");

                entity.Property(e => e.ContractObjectId)
                    .IsRequired()
                    .HasColumnName("contract_object_id")
                    .HasMaxLength(40)
                    .ForNpgsqlHasComment("Договор (GUID)");

                entity.Property(e => e.Gip)
                    .HasColumnName("gip")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("ГИП");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(2000)
                    .ForNpgsqlHasComment("Наименование здания/соооружения");

                entity.Property(e => e.Num)
                    .IsRequired()
                    .HasColumnName("num")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Порядковый номер (9-й атрибут обозначения)");

                entity.Property(e => e.ObjectId)
                    .IsRequired()
                    .HasColumnName("object_id")
                    .HasMaxLength(40);

                entity.Property(e => e.ObjectTime)
                    .HasColumnName("object_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'building'::character varying");

                entity.Property(e => e.ParentObjectId)
                    .IsRequired()
                    .HasColumnName("parent_object_id")
                    .HasMaxLength(40)
                    .ForNpgsqlHasComment("Идентификатор объекта в который входит здание/сооружение");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasColumnName("parent_type")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Тип объекта в который входит здание/сооружение");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");
            });

            modelBuilder.Entity<Constrpart>(entity =>
            {
                entity.ToTable("constrpart");

                entity.ForNpgsqlHasComment("Часть комплекса");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Код вида части комплекса (6-й атрибут обозначения)");

                entity.Property(e => e.Gip)
                    .HasColumnName("gip")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("ГИП");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(2000)
                    .ForNpgsqlHasComment("Наименование части комплекса");

                entity.Property(e => e.Num)
                    .IsRequired()
                    .HasColumnName("num")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Порядковый номер части комплекса (7-й атрибут обозначения)");

                entity.Property(e => e.ObjectId)
                    .IsRequired()
                    .HasColumnName("object_id")
                    .HasMaxLength(40);

                entity.Property(e => e.ObjectTime)
                    .HasColumnName("object_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'constrpart'::character varying");

                entity.Property(e => e.ParentObjectId)
                    .IsRequired()
                    .HasColumnName("parent_object_id")
                    .HasMaxLength(40)
                    .ForNpgsqlHasComment("Идентификатор объекта в который входит часть комплекса");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasColumnName("parent_type")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Тип объекта в который входит часть комплекса");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");
            });

            modelBuilder.Entity<Construction>(entity =>
            {
                entity.ToTable("construction");

                entity.ForNpgsqlHasComment("Комплекс");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gip)
                    .IsRequired()
                    .HasColumnName("gip")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("ГИП");

                entity.Property(e => e.OipKs)
                    .IsRequired()
                    .HasColumnName("oip_ks")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Шифр объекта (1-й атрибут обозначения)");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(2000)
                    .ForNpgsqlHasComment("Наименование комплекса");

                entity.Property(e => e.ObjectId)
                    .IsRequired()
                    .HasColumnName("object_id")
                    .HasMaxLength(40);

                entity.Property(e => e.ObjectTime)
                    .HasColumnName("object_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'construction'::character varying");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("contract");

                entity.ForNpgsqlHasComment("Договор");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Год составления договора");

                entity.Property(e => e.Dev)
                    .HasColumnName("dev")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Код ответственного филиала");

                entity.Property(e => e.InnerNumber)
                    .HasColumnName("inner_number")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Внутренний номер договора одной строкой");

                entity.Property(e => e.OipKs)
                    .IsRequired()
                    .HasColumnName("oip_ks")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Шифр объекта (1-й атрибут обозначения)");

                entity.Property(e => e.Ccode)
                    .HasColumnName("ccode")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Код заказчика (2-й атрибут обозначения)");

                entity.Property(e => e.Cdate)
                    .HasColumnName("cdate")
                    .HasColumnType("date")
                    .ForNpgsqlHasComment("Дата договора");

                entity.Property(e => e.Gip)
                    .HasColumnName("gip")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("ГИП");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(2000)
                    .ForNpgsqlHasComment("Наименование договора");

                entity.Property(e => e.Num)
                    .IsRequired()
                    .HasColumnName("num")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Номер договора (3-й атрибут обозначения)");

                entity.Property(e => e.ObjectId)
                    .IsRequired()
                    .HasColumnName("object_id")
                    .HasMaxLength(40);

                entity.Property(e => e.ObjectTime)
                    .HasColumnName("object_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'contract'::character varying");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");
            });

            modelBuilder.Entity<Docset>(entity =>
            {
                entity.ToTable("docset");

                entity.ForNpgsqlHasComment("Составной документ");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bcode)
                    .IsRequired()
                    .HasColumnName("bcode")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Код здания, сооружения (8-й атрибут обозначения)");

                entity.Property(e => e.Bnum)
                    .IsRequired()
                    .HasColumnName("bnum")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Порядковый номер позиции по генплану (9-й атрибут обозначения)");

                entity.Property(e => e.Bstage)
                    .HasColumnName("bstage")
                    .HasMaxLength(1000)
                    .ForNpgsqlHasComment("Этап строительства по договору");

                entity.Property(e => e.Ccode)
                    .IsRequired()
                    .HasColumnName("ccode")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Код заказчика (2-й атрибут обозначения)");

                entity.Property(e => e.Cipher)
                    .IsRequired()
                    .HasColumnName("cipher")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Шифр комплекта в соответствии с Регламентом Р-08");

                entity.Property(e => e.CipherDoc)
                    .HasColumnName("cipher_doc")
                    .HasMaxLength(20)
                    .ForNpgsqlHasComment("Шифр документа (12-й атрибут обозначения)");

                entity.Property(e => e.Cpcode)
                    .IsRequired()
                    .HasColumnName("cpcode")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Код части комплекса стройки (6-й атрибут обозначения)");

                entity.Property(e => e.Cpnum)
                    .IsRequired()
                    .HasColumnName("cpnum")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Порядковый № части комплекса стройки (7-й атрибут обозначения)");

                entity.Property(e => e.Cstage)
                    .HasColumnName("cstage")
                    .HasMaxLength(1000)
                    .ForNpgsqlHasComment("№ этапа по генподрядному договору");

                entity.Property(e => e.DevDep)
                    .IsRequired()
                    .HasColumnName("dev_dep")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Код подразделения-исполнителя внутри филиала");

                entity.Property(e => e.DevOrg)
                    .IsRequired()
                    .HasColumnName("dev_org")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Субъект разработки (5-й атрибут обозначения)");

                entity.Property(e => e.Gip)
                    .HasColumnName("gip")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("ГИП");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.IzmNum)
                    .HasColumnName("izm_num")
                    .ForNpgsqlHasComment("Номер изменения");

                entity.Property(e => e.Mark)
                    .IsRequired()
                    .HasColumnName("mark")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Марка основного комплекта (10-й атрибут обозначения)");

                entity.Property(e => e.MarkPath)
                    .HasColumnName("mark_path")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("№ части марки (11-й атрибут обозначения)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(2000)
                    .ForNpgsqlHasComment("Наименование с титульного листа");

                entity.Property(e => e.Num)
                    .IsRequired()
                    .HasColumnName("num")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Номер договора (3-й атрибут обозначения)");

                entity.Property(e => e.ObjectId)
                    .IsRequired()
                    .HasColumnName("object_id")
                    .HasMaxLength(40);

                entity.Property(e => e.ObjectTime)
                    .HasColumnName("object_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'docset'::character varying");

                entity.Property(e => e.OipKs)
                    .IsRequired()
                    .HasColumnName("oip_ks")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Шифр объекта (1-й атрибут обозначения)");

                entity.Property(e => e.ParentObjectId)
                    .IsRequired()
                    .HasColumnName("parent_object_id")
                    .HasMaxLength(40)
                    .ForNpgsqlHasComment("Идентификатор объекта которому принадлежит документ");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasColumnName("parent_type")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Тип объекта которому принадлежит документ");

                entity.Property(e => e.Pstage)
                    .IsRequired()
                    .HasColumnName("pstage")
                    .HasMaxLength(10)
                    .ForNpgsqlHasComment("Стадия проектирования (4-й атрибут обозначения)");

                entity.Property(e => e.IsActual)
                    .HasColumnName("is_actual");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Статус документа в исторической системе");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.ForNpgsqlHasComment("Документ в составе составного документа");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DevDep)
                    .IsRequired()
                    .HasColumnName("dev_dep")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Код подразделения-исполнителя внутри филиала");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Cipher)
                    .IsRequired()
                    .HasColumnName("cipher")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Обозначение документа в соответствии с Регламентом Р-08");

                entity.Property(e => e.CipherDoc)
                    .HasColumnName("cipher_doc")
                    .HasMaxLength(20)
                    .ForNpgsqlHasComment("Шифр документа (12-й атрибут обозначения)");

                entity.Property(e => e.IzmNum)
                    .HasColumnName("izm_num")
                    .ForNpgsqlHasComment("Номер изменения");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(2000)
                    .ForNpgsqlHasComment("Наименование с титульного листа");

                entity.Property(e => e.ObjectId)
                    .IsRequired()
                    .HasColumnName("object_id")
                    .HasMaxLength(40);

                entity.Property(e => e.ObjectTime)
                    .HasColumnName("object_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'document'::character varying");

                entity.Property(e => e.ParentObjectId)
                    .IsRequired()
                    .HasColumnName("parent_object_id")
                    .HasMaxLength(40)
                    .ForNpgsqlHasComment("Идентификатор объекта которому принадлежит документ");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasColumnName("parent_type")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Тип объекта которому принадлежит документ");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Статус документа в исторической системе");

                entity.Property(e => e.IsActual)
                    .HasColumnName("is_actual")
                    .ForNpgsqlHasComment("Признак актуальности документа");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("file");

                entity.ForNpgsqlHasComment("Файл документа");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FileModifyTime)
                    .HasColumnName("file_modify_time")
                    .HasColumnType("timestamp with time zone")
                    .ForNpgsqlHasComment("Дата/время последней модификации файла");

                entity.Property(e => e.FileTypeId)
                    .HasColumnName("file_type_id")
                    .ForNpgsqlHasComment("Тип файла: 0-UNKNOWN, 1-Скан с подписями, 2-Файл в редартируемом формате, (>=3)-Зарезервировано");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("file_name")
                    .HasMaxLength(2000)
                    .ForNpgsqlHasComment("Имя файла без пути");

                entity.Property(e => e.FileSize)
                    .HasColumnName("file_size")
                    .ForNpgsqlHasComment("Размер файла в байтах");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'file'::character varying");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.ParentObjectId)
                    .IsRequired()
                    .HasColumnName("parent_object_id")
                    .HasMaxLength(40)
                    .ForNpgsqlHasComment("Идентификатор объекта которому принадлежит файл");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasColumnName("parent_type")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Тип объекта которому принадлежит файл");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("invoice");

                entity.ForNpgsqlHasComment("Накладная");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnName("invoice_date")
                    .HasColumnType("date")
                    .ForNpgsqlHasComment("Дата накладной");

                entity.Property(e => e.InvoiceNum)
                    .IsRequired()
                    .HasColumnName("invoice_num")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Номер накладной");

                entity.Property(e => e.ObjectId)
                    .IsRequired()
                    .HasColumnName("object_id")
                    .HasMaxLength(40);

                entity.Property(e => e.ObjectTime)
                    .HasColumnName("object_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasColumnName("object_type")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'invoice'::character varying");

                entity.Property(e => e.Recipient)
                    .IsRequired()
                    .HasColumnName("recipient")
                    .HasMaxLength(2000)
                    .ForNpgsqlHasComment("Получатель");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(100)
                    .ForNpgsqlHasComment("Статус накладной в исторической системе");
            });

            modelBuilder.Entity<ObjRef>(entity =>
            {
                entity.ToTable("obj_ref");

                entity.ForNpgsqlHasComment("Ссылка на документ из другого документа");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.OwnerId)
                    .HasColumnName("owner_id")
                    .ForNpgsqlHasComment("Идентификатор документа которому принадлежит ссылка (аналог parent_object_id)");

                entity.Property(e => e.ParentObjectId)
                    .IsRequired()
                    .HasColumnName("parent_object_id")
                    .HasMaxLength(40)
                    .ForNpgsqlHasComment("Идентификатор объекта в составе которого находится ссылка");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasColumnName("parent_type")
                    .HasMaxLength(30)
                    .ForNpgsqlHasComment("Тип объекта в составе которого находится ссылка");

                entity.Property(e => e.RefObjectId)
                    .IsRequired()
                    .HasColumnName("ref_object_id")
                    .HasMaxLength(40)
                    .ForNpgsqlHasComment("Иденификатор ссылочного объекта");

                entity.Property(e => e.SrcPacketId).HasColumnName("src_packet_id");
            });

            modelBuilder.Entity<Packet>(entity =>
            {
                entity.ToTable("packet");

                entity.ForNpgsqlHasComment("Пакет");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FileContent)
                    .HasColumnName("file_content")
                    .HasColumnType("xml");

                entity.Property(e => e.FileModificationTime)
                    .HasColumnName("file_modification_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("file_name")
                    .HasMaxLength(255);

                entity.Property(e => e.FileSha256)
                    .IsRequired()
                    .HasColumnName("file_sha256")
                    .HasMaxLength(44);

                entity.Property(e => e.FileSize).HasColumnName("file_size");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StatusTime)
                    .HasColumnName("status_time")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<PacketLog>(entity =>
            {
                entity.ToTable("packet_log");

                entity.ForNpgsqlHasComment("Журнал изменения статусов пакета");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MsgText)
                    .HasColumnName("msg_text")
                    .HasMaxLength(2048);

                entity.Property(e => e.MsgTime)
                    .HasColumnName("msg_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.MsgTypeId).HasColumnName("msg_type_id");

                entity.Property(e => e.PacketId).HasColumnName("packet_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Packet)
                    .WithMany(p => p.PacketLog)
                    .HasForeignKey(d => d.PacketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("packet_log_packet_id");
            });
        }
    }
}
