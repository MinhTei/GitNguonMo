<?php


// Xử lý khi form được submit
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Lấy dữ liệu từ form
    $mssv22 = $_POST['mssv'] ?? '';
    $hoten22 = $_POST['hoten'] ?? '';
    $gioitinh22 = $_POST['gioitinh'] ?? '';
    
    // Xử lý checkbox sách - ghép thành chuỗi
    $sach_array = $_POST['sach'] ?? [];
    $sach22 = implode(", ", $sach_array);
    
    // Xử lý upload file
    if (isset($_FILES['hinhanh']) && $_FILES['hinhanh']['error'] == 0) {
        // Tạo thư mục uploads nếu chưa có
        if (!file_exists('uploads')) {
            mkdir('uploads');
        }
        
        // Lấy thông tin file
        $file_name = $_FILES['hinhanh']['name'];
        $file_tmp = $_FILES['hinhanh']['tmp_name'];
        
        // Di chuyển file vào thư mục uploads
        $upload_path = 'uploads/' . $file_name;
        move_uploaded_file($file_tmp, $upload_path);
        $hinhanh22 = $upload_path;
    }
}
?>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Thông tin sinh viên</title>
</head>
<body>
    <h2>THÔNG TIN SINH VIÊN OKE</h2>
    
    <form method="POST" enctype="multipart/form-data">
        <p>
            <label>MSSV:</label><br>
            <input type="text" name="mssv" required>
        </p>
        
        <p>
            <label>Họ tên:</label><br>
            <input type="text" name="hoten" required>
        </p>
        
        <p>
            <label>Giới tính:</label><br>
            <input type="radio" name="gioitinh" value="Nam" required> Nam
            <input type="radio" name="gioitinh" value="Nữ"> Nữ
        </p>
        
        <p>
            <label>Danh sách các sách thích:</label><br>
            <input type="checkbox" name="sach[]" value="Thể thao"> Thể thao
            <input type="checkbox" name="sach[]" value="Du lịch"> Du lịch
        </p>
        
        <p>
            <label>Hình ảnh:</label><br>
            <input type="file" name="hinhanh">
        </p>
        
        <p>
            <button type="submit">Gửi</button>
        </p>
    </form>
    
    <hr>
    
    <?php if ($_SERVER["REQUEST_METHOD"] == "POST"): ?>
        <h3>Xuất ra các kết quả nhập gồm:</h3>
        <p><b>MSSV:</b> <?php echo $mssv22; ?></p>
        <p><b>Họ tên:</b> <?php echo $hoten22; ?></p>
        <p><b>Giới tính:</b> <?php echo $gioitinh22; ?></p>
        <p><b>Danh sách các sở thích:</b> <?php echo $sach22; ?></p>
        <p><b>Hình ảnh sự vừa upload:</b></p>
        <?php if ($hinhanh22): ?>
            <img src="<?php echo $hinhanh22; ?>" width="200">
        <?php else: ?>
            <p>Chưa có ảnh</p>
        <?php endif; ?>
    <?php endif; ?>
</body>
</html>