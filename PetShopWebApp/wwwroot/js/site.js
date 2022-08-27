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
    const data = { id: this.dataset.id };
    $(formdata).each(function (index, obj) {
        data[obj.name] = obj.value;
    });
    $.post("/Home/AddAnimalComment/", data, function (data) {
        $('#comments').prepend(`
            <div class="card-body">
                <div class="card-title fs-5">${data.text}</div>
                    <div class="d-flex justify-content-between">
                        <small>Auther: ${data.auther}</small>
                        <small>Created At: ${data.createdDate}</small>
                    </div>
                </div>
            </div>
        `);
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