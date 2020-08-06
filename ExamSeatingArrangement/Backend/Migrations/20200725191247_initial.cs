using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ExamSeatingArrangement2020.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortForm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SubjectCode = table.Column<string>(nullable: false),
                    SubjectName = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Session = table.Column<string>(nullable: true),
                    Order = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.UniqueConstraint("AK_Exams_SubjectCode", x => x.SubjectCode);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    IsFinished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.UniqueConstraint("AK_Files_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PdfRectangleModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Llx = table.Column<int>(nullable: false),
                    Lly = table.Column<int>(nullable: false),
                    Urx = table.Column<int>(nullable: false),
                    Ury = table.Column<int>(nullable: false),
                    Region = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfRectangleModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoomNumber = table.Column<string>(nullable: true),
                    SeatNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<string>(nullable: false),
                    SemesterMonths = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                    table.UniqueConstraint("AK_Semester_Date", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    IsAuthorized = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RegisterNumber = table.Column<string>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seatings", x => x.Id);
                    table.UniqueConstraint("AK_Seatings_RegisterNumber", x => x.RegisterNumber);
                    table.ForeignKey(
                        name: "FK_Seatings_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentExams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DepartmentId = table.Column<int>(nullable: false),
                    ExamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentExams", x => x.Id);
                    table.UniqueConstraint("AK_DepartmentExams_DepartmentId_ExamId", x => new { x.DepartmentId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_DepartmentExams_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomExams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(nullable: false),
                    ExamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomExams", x => x.Id);
                    table.UniqueConstraint("AK_RoomExams_RoomId_ExamId", x => new { x.RoomId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_RoomExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomExams_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatingExams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SeatingId = table.Column<int>(nullable: false),
                    ExamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatingExams", x => x.Id);
                    table.UniqueConstraint("AK_SeatingExams_SeatingId_ExamId", x => new { x.SeatingId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_SeatingExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatingExams_Seatings_SeatingId",
                        column: x => x.SeatingId,
                        principalTable: "Seatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatingRooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SeatingId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatingRooms", x => x.Id);
                    table.UniqueConstraint("AK_SeatingRooms_SeatingId_RoomId", x => new { x.SeatingId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_SeatingRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatingRooms_Seatings_SeatingId",
                        column: x => x.SeatingId,
                        principalTable: "Seatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentExams_ExamId",
                table: "DepartmentExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomExams_ExamId",
                table: "RoomExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatingExams_ExamId",
                table: "SeatingExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatingRooms_RoomId",
                table: "SeatingRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Seatings_DepartmentId",
                table: "Seatings",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentExams");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "PdfRectangleModels");

            migrationBuilder.DropTable(
                name: "RoomExams");

            migrationBuilder.DropTable(
                name: "SeatingExams");

            migrationBuilder.DropTable(
                name: "SeatingRooms");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Seatings");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
