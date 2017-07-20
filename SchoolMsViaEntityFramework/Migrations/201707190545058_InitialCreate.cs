namespace SchoolMsViaEntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        Title = c.String(),
                        Credits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EnrollmentDate = c.DateTime(nullable: false),
                        Enrollment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enrollment", t => t.Enrollment_Id)
                .Index(t => t.Enrollment_Id);
            
            CreateTable(
                "dbo.EnrollmentCourse",
                c => new
                    {
                        Enrollment_Id = c.Int(nullable: false),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Enrollment_Id, t.Course_CourseId })
                .ForeignKey("dbo.Enrollment", t => t.Enrollment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Enrollment_Id)
                .Index(t => t.Course_CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "Enrollment_Id", "dbo.Enrollment");
            DropForeignKey("dbo.EnrollmentCourse", "Course_CourseId", "dbo.Course");
            DropForeignKey("dbo.EnrollmentCourse", "Enrollment_Id", "dbo.Enrollment");
            DropIndex("dbo.EnrollmentCourse", new[] { "Course_CourseId" });
            DropIndex("dbo.EnrollmentCourse", new[] { "Enrollment_Id" });
            DropIndex("dbo.Student", new[] { "Enrollment_Id" });
            DropTable("dbo.EnrollmentCourse");
            DropTable("dbo.Student");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Course");
        }
    }
}
