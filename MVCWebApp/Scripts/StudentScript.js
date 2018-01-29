function ClearText()
{
    $('input[type=text]').each(function ()
    {
        $(this).val('');
    });
    $("#ddlCourse").val('--SELECT--'); // Select first option
}

function Validate()
{
    var Name = $("#name").val();
    var Address = $("#address").val();
    var Gender = $("input[name='rdGender']:checked").val();
    var Phone = $("#phone").val();
    var course = $('#ddlCourse option:selected').val();
    if (Name != "" && Address != "" && Gender != "" && course != 0)
    {
        if (Phone.length == 10)
        {
            return true;
        }
        else
        {
            //alert('Invalid phone number');
            $("#InvalidPhoneAlert").show().fadeOut(3500);
        }
    }
    else
    {
        //alert('Please enter all details');
        $("#ErrorAlert").show().fadeOut(3500);
        return false;
    }
}

function AddRecord()
{
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
        success: function ()
        {
            //alert("Student details added successfully");
            $("#SuccessAlert").fadeOut(3500);
            window.location.href = "/Home/Index";
        },
        error: function ()
        {
            alert("Some error occured while adding record.");
            $('#myModal').modal('hide');
        }
    });
}

function LoadRecords()
{
    $.ajax({
        type: 'Get',
        url: '/Home/Index',
        contentType: "application/json; charset=utf-8",
        success: function ()
        {
            $('#myModal').modal('hide');
        },
        error: function ()
        {
            $('#myModal').modal('hide');
        }
    });
}

function EditStudent(id)
{
    console.log(id);
    //Using Ajax request call /Home/GetStudent method and insert data in Db
    $.ajax({
        type: 'Get',
        url: '/Home/GetStudent',
        data: id,
        contentType: "application/json; charset=utf-8",
        success: function (data)
        {
            //alert("Student details added successfully");
            $("#SuccessAlert").show();
            console.log(data);
        },
        error: function ()
        {
            console.log("Some error occured while adding record.");
            $('#myModal').modal('hide');
            return false;
        }
    });
}

$("#btnAdd").click(function ()
{
    if (Validate())
    {
        AddRecord();
    }
});

$("#btnClear").click(function ()
{
    ClearText();
});