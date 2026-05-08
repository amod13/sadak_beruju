function showImagePreview(imageUploader, previewImage) {

    if (imageUploader.file && imageUploader.file[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.file[0]);
    }
};

//public Employee()'//construrctor
//{
//    //Default image location
//    ImagePath = "~/AppFiles/Images/default.png";
//}



//<img src"@ulr.content(Model.ImagePath)" id = "imagePriview" />
//    <input type="file" name="ImageUpload" accept="image/jpg" onchage="showImagePreview(this,document.getelementbyid('imagePriview'))">