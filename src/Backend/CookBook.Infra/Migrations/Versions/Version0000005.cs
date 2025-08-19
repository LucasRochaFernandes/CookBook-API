using FluentMigrator;

namespace CookBook.Infra.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_REFRESH_TOKEN)]
public class Version0000005 : MigrationBase
{
    public override void Up()
    {
        CreateTable("RefreshTokens")
            .WithColumn("Value").AsString().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_RefreshToken_User_Id", "Users", "Id");
    }
}
