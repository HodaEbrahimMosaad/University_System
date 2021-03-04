
$(function () {
    
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
    $("#theme-loader").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#theme-loader").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#theme-loader").addClass('hide');
    });
});

function showInPopup(url, title) {
    console.log("ajax" + url + title);
    $.ajax({
        
        type: "GET",
        url: url,
        success: function (res) {
            //$("#form-modal .modal-body form").reset();
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
            if (title == "Edit Instructor") {
                $(".pass").hide();
            }

        },
        error: function () {

        }
    })

}


jQueryAjaxPost = form => {

    try {

        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#ViewAll').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    //document.getElementById("courseForm").reset();
                    $('#form-modal').modal('hide');
                    $.notify('Submitted Successfuly', { globalPosition: 'top center', className: 'success' })

                }
                else
                    $('#form-modal .modal-body').html(res.html);

            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event

        return false;
    } catch (ex) {
        console.log(ex)
    }
}

function showInPopupv2(url, title, lec=0) {
    var siteurl = window.location.href;// Returns full URL (https://example.com/path/example.html)
    var crsId = Number.parseInt(siteurl.slice(-1));
    var mydata;
    if (lec != 0) {
        mydata = { crsid: crsId, lecid: lec }
        console.log("lec " + lec + "co=rs" + crsId)
    }
    else {
        mydata = { crsid: crsId }
    }
    console.log(crsId + lec);
    $.ajax({
        type: "GET",
        url: url,
        data: mydata,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
            if (title == "Edit Instructor") {
                $(".pass").hide();
            }

        },
        error: function (err) {
            console.log("error");
            console.log(err)
        }
    })
    
}


    jQueryAjaxPostv2 = form => {
    
        try {
            
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {

                    $('#form-modal').modal('hide');
                    console.log("sucsss")
                    $('#courseDetails').load(document.URL + ' #courseDetails');

                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
}


jQueryAjaxPostIns = form => {

    try {

        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    //$('#ViewAll').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    //document.getElementById("courseForm").reset();
                    $('#form-modal').modal('hide');
                    //insContent
                    $('#insContent').load(document.URL + ' #insContent');
                    $.notify('Submitted Successfuly', { globalPosition: 'top center', className: 'success' })
                }
                else
                    $('#form-modal .modal-body').html(res.html);

            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event

        return false;
    } catch (ex) {
        console.log(ex)
    }

}

jQueryAjaxPostPosts = form => {

 try {

 $.ajax({
type: 'POST',
url: form.action,
data: new FormData(form),
contentType: false,
processData: false,
success: function (res) {
if (res.isValid) {
//$('#ViewAll').html(res.html)
$('#form-modal .modal-body').html('');
$('#form-modal .modal-title').html('');
//document.getElementById("courseForm").reset();
$('#form-modal').modal('hide');
//insContent
$(' #mydivTopost').load(document.URL + ' #mydivTopost');
}
else
$('#form-modal .modal-body').html(res.html);

 },
error: function (err) {
console.log(err)
}
})
//to prevent default form submit event


 return false;
} catch (ex) {
console.log(ex)
}
}
jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    alert("h")
                    $('#ViewAll').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
    //prevent default form submit event
    return false;
}