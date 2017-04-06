
$(".caret").click(function () {
    $(".myDropdownMenu").toggle();
});

console.log('hii');

function toggleCategories(e) {
    var hdr_id = e; //$("#hdrCtg").attr('id');
    $("#ctgBdy_" + hdr_id).toggle();
}
