//  Add Like Function
$('.btn-like').click(
    function (btn) {
        const id = btn.currentTarget.dataset.animal;
        $.post("/Home/AddAnimalLike/" + id, function (data) { $("#like_" + id).html(data); });
        btn.currentTarget.setAttribute('disabled', '');
    }
)
// Add Comment Function
const form = $('#newComment');
form.submit(function (e) {
    e.preventDefault()
    const formdata = $(this).serializeArray()
    const data = {
        id: this.dataset.id,
        [formdata[0].name]: formdata[0].value,
        [formdata[1].name]: formdata[1].value
    };
    $.post("/Home/AddAnimalComment/", data, function (data) {
        const comment = $(`
            <div class="card-body">
                <div class="card-title fs-5">${data.text}</div>
                    <div class="d-flex justify-content-between">
                        <small>Auther: ${data.auther}</small>
                        <small>Created At: ${data.createdDate}</small>
                    </div>
                </div>
            </div>
        `)
        $('#comments').prepend(comment);
        $(comment).hide();
        $(comment).slideDown();
        form.trigger('reset');
    });
})
// Menu Toggle
$('.navbar-toggler').click(
    function (btn) {
        const isOpen = !btn.currentTarget.ariaExpanded;
        btn.currentTarget.ariaExpanded = isOpen;
        const menu = $(btn.currentTarget.dataset.bsTarget);
        menu.toggleClass('show', isOpen);
        menu.slideToggle(isOpen);
    }
)
// Auto Set Category
$('#selectCategory').change(
    function (select) {
        $(this.parentNode).trigger('submit');
    }
)