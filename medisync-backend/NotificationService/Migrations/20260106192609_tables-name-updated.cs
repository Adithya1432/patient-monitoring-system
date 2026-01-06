using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationService.Migrations
{
    /// <inheritdoc />
    public partial class tablesnameupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationRetry_Notification_NotificationId",
                table: "NotificationRetry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTemplate",
                table: "NotificationTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationRetry",
                table: "NotificationRetry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.RenameTable(
                name: "NotificationTemplate",
                newName: "NotificationTemplates");

            migrationBuilder.RenameTable(
                name: "NotificationRetry",
                newName: "NotificationRetries");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTemplate_TemplateName_Channel",
                table: "NotificationTemplates",
                newName: "IX_NotificationTemplates_TemplateName_Channel");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationRetry_NotificationId_RetryCount",
                table: "NotificationRetries",
                newName: "IX_NotificationRetries_NotificationId_RetryCount");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_Status",
                table: "Notifications",
                newName: "IX_Notifications_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_RecipientId",
                table: "Notifications",
                newName: "IX_Notifications_RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_AppointmentId",
                table: "Notifications",
                newName: "IX_Notifications_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTemplates",
                table: "NotificationTemplates",
                column: "TemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationRetries",
                table: "NotificationRetries",
                column: "RetryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationRetries_Notifications_NotificationId",
                table: "NotificationRetries",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationRetries_Notifications_NotificationId",
                table: "NotificationRetries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTemplates",
                table: "NotificationTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationRetries",
                table: "NotificationRetries");

            migrationBuilder.RenameTable(
                name: "NotificationTemplates",
                newName: "NotificationTemplate");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "NotificationRetries",
                newName: "NotificationRetry");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTemplates_TemplateName_Channel",
                table: "NotificationTemplate",
                newName: "IX_NotificationTemplate_TemplateName_Channel");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_Status",
                table: "Notification",
                newName: "IX_Notification_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_RecipientId",
                table: "Notification",
                newName: "IX_Notification_RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_AppointmentId",
                table: "Notification",
                newName: "IX_Notification_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationRetries_NotificationId_RetryCount",
                table: "NotificationRetry",
                newName: "IX_NotificationRetry_NotificationId_RetryCount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTemplate",
                table: "NotificationTemplate",
                column: "TemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "NotificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationRetry",
                table: "NotificationRetry",
                column: "RetryId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationRetry_Notification_NotificationId",
                table: "NotificationRetry",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "NotificationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
