CKEDITOR.editorConfig = function (config) {
    // cấu hình tiếng việt cho CkEditor
    config.language = 'vi';
    // Bỏ thể br đầu tiên 
    config.enterMode = CKEDITOR.ENTER_BR;
    // Tích hợp CKFinder để Upload ảnh 
    config.filebrowserBrowseUrl = '/Scripts/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Scripts/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Scripts/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};
