namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.AttendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId)
                .Index(t => t.AttendeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendance", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Attendance", "AttendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendance", new[] { "AttendeeId" });
            DropIndex("dbo.Attendance", new[] { "CourseId" });
            DropTable("dbo.Attendance");
        }
    }
}
