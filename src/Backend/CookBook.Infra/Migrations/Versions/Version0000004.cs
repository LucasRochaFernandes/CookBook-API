using FluentMigrator;

namespace CookBook.Infra.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_USER_FORGOT_PASSWORD)]
public class Version0000004 : MigrationBase
{
    public override void Up()
    {
        CreateTable("CodeToPerformActions")
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("Value").AsString().NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_CodeToPerformActon_User_Id", "Users", "Id");
    }
}
