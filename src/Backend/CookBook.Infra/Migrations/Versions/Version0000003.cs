using FluentMigrator;

namespace CookBook.Infra.Migrations.Versions;

[Migration(DatabaseVersions.IMAGES_FOR_RECIPES)]
public class Version0000003 : MigrationBase
{
    public override void Up()
    {
        Alter.Table("Recipes").AddColumn("ImageIdentifier").AsString().Nullable();
    }
}
