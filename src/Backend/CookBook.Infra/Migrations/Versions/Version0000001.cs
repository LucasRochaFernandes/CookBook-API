using FluentMigrator;

namespace CookBook.Infra.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_USER, "Create table to persist user")]
public class Version0000001 : MigrationBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("isActive").AsBoolean().NotNullable()
            .WithColumn("Name").AsString(80).NotNullable()
            .WithColumn("Email").AsString(80).NotNullable().Unique()
            .WithColumn("Password").AsString(2000).NotNullable()
            .WithColumn("UpdatedAt").AsDateTime().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable();
    }
}
