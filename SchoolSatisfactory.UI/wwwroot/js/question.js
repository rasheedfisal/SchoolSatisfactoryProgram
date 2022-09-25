
$(function () {
    if (localStorage.SchoolId !== "null" && localStorage.rateNo !== "null") {

        $.post('/Home/GetQuestions', { SchoolNo: localStorage.SchoolId, RateNo: localStorage.rateNo }, function (data) {
            if (data !== null && data.length > 0) {
                $.each(data, function (i, e) {
                    var parentQ = $(`<div class="row col-12 animate__animated animate__slideInUp">`);
                    var question = $(`<p class="fw-bold">${e.questionNameAr}</p>`);
                    var questionId = $(`<input type="hidden" id="hdn${i}" value="${e.id}">`);
                    $.each(e.questionAnswers, function (ui, x) {
                        var div = $(`<div class="form-check mb-2">`);
                        div.append(`<input class="form-check-input" type="radio" name="group_${i}" id="check_${ui}" value="${x.id}" />`);
                        div.append(`<label class="form-check-label" for="${ui}check">${x.answerNameAr}</label>`);
                        parentQ.append(div);
                    });
                    parentQ.prepend(questionId);
                    parentQ.prepend(question);
                    $('#questionForm').append(parentQ);
                });
                var submitBtn = $(`<div class="text-start">`);
                submitBtn.append('<button type="button" onClick="gotoOthers()" id="btnSaveSurvay" class="btn btn-primary">اضافة الاستطلاع</button>');
                $('#questionForm').append(submitBtn);
            } else {
                window.location.href = "/Home/QuestionsNotFound";
            }
        });
    } else {
        window.location.href = "/Home/Index"
    }
})

gotoOthers = () => {
    var radio_groups = {}
    var if_checked = false;

    $(":radio").each(function (e) {
        radio_groups[this.name] = true;
        //radio_groups[this.value] = e.value;
    })
    for (group in radio_groups) {
        if_checked = !!$(":radio[name='" + group + "']:checked").length
        // alert(group + (if_checked ? ' has checked radios' : ' does not have checked radios'))
        if (if_checked === false) {
            $('html, body').animate({
                scrollTop: $(":radio[name='" + group + "']").parent().parent().parent().offset().top
            }, 0);
            Toast.fire({
                type: 'error',
                title: 'الرجاء اجابة هذا السؤال!'
            })
            break;
        }
    }
    if (if_checked) {
        $('#survay').slideUp(500);
        $('#Others').show("fast")
        $('html, body').animate({
            scrollTop: $("#Others").parent().parent().offset().top
        }, 0);
    }
}


PostSurvay = form => {
    var radio_groups = {}
    var selectedItems = [];


    $(":radio").each(function (e) {
        radio_groups[this.name] = true;
        //radio_groups[this.value] = e.value;
    })
    for (group in radio_groups) {
        if_checked = !!$(":radio[name='" + group + "']:checked").length
        // alert(group + (if_checked ? ' has checked radios' : ' does not have checked radios'))
        if (if_checked === false) {
            $('html, body').animate({
                scrollTop: $(":radio[name='" + group + "']").parent().parent().parent().offset().top
            }, 0);
            Toast.fire({
                type: 'error',
                title: 'الرجاء اجابة هذا السؤال!'
            })
            break;
        } else {
            let answerId = $(":radio[name='" + group + "']:checked").val();
            let questionId = $("#hdn" + $(":radio[name='" + group + "']:checked").attr('name').split('_')[1]).val();
            let Items = {
                "AnswerNo": answerId,
                "QuestionNo": questionId
            }

            selectedItems.push(Items);
        }
    }

    $.post('/Home/PostSurvay', { Survaylist: selectedItems, SchoolNo: localStorage.SchoolId, RateNo: localStorage.rateNo, ClientName: localStorage.clientname, Qualification: localStorage.qualification, Others:$('#comment').val() }, function (data) {
        if (data.success) {
            $('#survay-success').show(1000);
            $('#Others').hide(1000);
            $('html, body').animate({
                scrollTop: $("#survay-success").offset().top
            }, 0);
            Toast.fire({
                type: 'success',
                title: 'تم اضافة الاستطلاع بنجاح'
            })
            localStorage.SchoolId = null;
            localStorage.rateNo = null;
            localStorage.clientname = null;
            localStorage.qualification = null;
        }
    })

    return false;
}

