using Microsoft.EntityFrameworkCore.Migrations;

namespace Alura.ByteBank.Dados.Migrations
{
    public partial class PopularBancoByteBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO `agencia` VALUES (null, 123, 'Agencia Central', 'Rua: Pedro Alvares Cabral, 11','3d16f1e2-de44-4adb-bf25-228882d113fe')");
            migrationBuilder.Sql("INSERT INTO `agencia` VALUES (null, 321, 'Agencia Flores', 'Rua: Odete Roitman, 84','f53217cf-9c9a-420e-8ab9-6de330b6f4ae')");

            migrationBuilder.Sql("INSERT INTO `cliente` VALUES (null, '307.522.040-09', 'André Silva', 'Developer','9c207ec1-eacb-4bdc-b413-92419335f3cf')");
            migrationBuilder.Sql("INSERT INTO `cliente` VALUES (null, '817.452.000-70', 'João Pedro', 'Developer','5b6525fc-7c0a-49a6-b17a-26c84cea3f6a')");
            migrationBuilder.Sql("INSERT INTO `cliente` VALUES (null, '224.182.250-70', 'José Neves', 'Atleta de Poker','43b3dcd1-eaf1-492a-89a2-ff98a0926b66')");


            migrationBuilder.Sql("INSERT INTO `conta_corrente` VALUES (null, 4159, 1,1,300,'f19fabe4-e412-4877-94c5-ad53797ff9b7', '00000000-0000-0000-0000-000000000000')");
            //migrationBuilder.Sql("INSERT INTO `conta_corrente` VALUES (null, 1789, 2,2,400,'c50b895b-a2b7-4dc8-8235-5bb6a3738a3c', '00000000-0000-0000-0000-000000000000')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete FROM `cliente` where id<>null ");
            migrationBuilder.Sql("Delete FROM `agencia` where id<>null ");
            migrationBuilder.Sql("Delete FROM `conta_corrente` where id<>null ");
        }
    }
}
