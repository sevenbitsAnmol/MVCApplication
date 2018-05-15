function ClearText() {
    $('input[type=text]').each(function () {
        $(this).val('');
    });
    $("#ddlCourse").val('--SELECT--'); // Select first option
}

function Validate() {
    var Name = $("#name").val();
    var Address = $("#address").val();
    var Gender = $("input[name='rdGender']:checked").val();
    var Phone = $("#phone").val();
    var course = $('#ddlCourse option:selected').val();
    if (Name != "" && Address != "" && Gender != "" && course != 0) {
        if (Phone.length == 10) {
            return true;
        }
        else {
            //alert('Invalid phone number');
            $("#InvalidPhoneAlert").show().fadeOut(3500);
        }
    }
    else {
        //alert('Please enter all details');
        $("#ErrorAlert").show().fadeOut(3500);
        return false;
    }
}

function AddRecord() {
    var empObj =
        {
            CourseID: $('#ddlCourse option:selected').val(),
            Name: $("#name").val(),
            Address: $("#address").val(),
            Gender: $("input[name='rdGender']:checked").val(),
            Phone: $("#phone").val(),
        };

    //Using Ajax request call /Home/Add method and insert data in Db
    $.ajax({
        type: 'Post',
        url: '/Home/Add',
        data: JSON.stringify(empObj),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //alert("Student details added successfully");
            $("#SuccessAlert").fadeOut(3500);
            $('#myModal').modal('hide');
            $("#tobodyId").remove();
            for (var i = 0; i < 10; i++) {
                var StudentList = "<tr><td>" + data[i].Name + "</td>"
                    + "<td>" + data[i].Address + "</td>" +
                    "<td>" + data[i].Gender + "</td>" +
                    "<td>" + data[i].Phone + "</td>" +
                    "<td colspan='3'><a href='/Home/Edit/" + data[i].Id + "' onclick = 'EditStudent(" + data[i].Id + "); return false;'>Edit | </a>" +
                    "<a href='/Home/Details/" + data[i].Id + "' >Details | </a>" +
                    "<a href='/Home/Delete/" + data[i].Id + "' >Delete</a></td></tr></tbody></table>";
                $(".table").append(StudentList);
            }
            //window.location.href = "/Home/Index";
        },
        error: function () {
            alert("Some error occured while adding record.");
            $('#myModal').modal('hide');
        }
    });
}

function LoadRecords() {
    $.ajax({
        type: 'Get',
        url: '/Home/LoadStudentList',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            for (var i = 0; i < 10; i++) {
                var StudentList = "<tr><td>" + data[i].Name + "</td>"
                    + "<td>" + data[i].Address + "</td>" +
                    "<td>" + data[i].Gender + "</td>" +
                    "<td>" + data[i].Phone + "</td>" +
                    "<td colspan='3'><a href='/Home/Edit/" + data[i].Id + "' onclick = 'EditStudent(73); return false;'>Edit | </a>" +
                    "<a href='/Home/Details/" + data[i].Id + "' >Details | </a>" +
                    "<a href='/Home/Delete/" + data[i].Id + "' >Delete</a></td></tr></tbody></table>";
                $(".table").append(StudentList);
            }
            $('#myModal').modal('hide');
        },
        error: function () {
            $('#myModal').modal('hide');
        }
    });
}

function EditStudent(id) {
    //console.log(id);
    var ID = id;
    //Using Ajax request call /Home/GetStudent method and get Student data from Db
    $.ajax({
        type: 'Get',
        url: '/Home/GetStudent/' + ID,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $('#myModal').modal('show');
            $('#name').val(result[0].Name);
            $('#address').val(result[0].Address);
            $('#phone').val(result[0].Phone);
            $('#Id').val(result[0].Id);
            $("input[name=rdGender][value=" + result[0].Gender + "]").prop("checked", true);
            $('select[name="DdlCourse"]').find('option[value="' + result[0].CourseId + '"]').prop("selected", true);
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function () {
            console.log("Some error occured while getting record.");
            $('#myModal').modal('hide');
            return false;
        }
    });
}

function UpdateStudent() {
    var empObj =
        {
            CourseID: $('#ddlCourse option:selected').val(),
            Id: $('#Id').val(),
            Name: $("#name").val(),
            Address: $("#address").val(),
            Gender: $("input[name='rdGender']:checked").val(),
            Phone: $("#phone").val(),
        };
    //console.log($('#Id').val());
    //Using Ajax request call /Home/Update method and update data in Db
    $.ajax({
        type: 'Post',
        url: '/Home/Update',
        data: JSON.stringify(empObj),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //alert("Student details added successfully");
            $("#SuccessAlert").fadeOut(3500);
            $('#myModal').modal('hide');
            $(".table tbody > tr").remove();
            for (var i = 0; i < 10; i++) {
                var StudentList = "<tr><td>" + data[i].Name + "</td>"
                    + "<td>" + data[i].Address + "</td>" +
                    "<td>" + data[i].Gender + "</td>" +
                    "<td>" + data[i].Phone + "</td>" +
                    "<td colspan='3'><a href='/Home/Edit/" + data[i].Id + "' onclick = 'EditStudent(" + data[i].Id + "); return false;'>Edit | </a>" +
                    "<a href='/Home/Details/" + data[i].Id + "' >Details | </a>" +
                    "<a href='/Home/Delete/" + data[i].Id + "' >Delete</a></td></tr></table>";
                $(".table tbody").append(StudentList);
            }
            //window.location.href = "/Home/Index";
        },
        error: function () {
            alert("Some error occured while adding record.");
            $('#myModal').modal('hide');
        }
    });
}

//Events
$("#btnAdd").click(function () {
    if (Validate()) {
        AddRecord();
        ClearText();
    }
});

$('#btnOpenAdd').click(function () {
    $('#btnUpdate').hide();
    $('#btnAdd').show();
});

$("#btnClear").click(function () {
    ClearText();
});

$('#btnUpdate').click(function () {
    if (Validate()) {
        UpdateStudent();
        ClearText();
    }
});