Hệ thống quản lý bán hàng cho công ty TNHH phát triển công nghệ Quang Anh
1. Cách cài đặt
  - Clone project và open, nên chuyển sang develop branch
  - Vào file appsettings.json tại các project: BackendApi, IdentityServer, Data và sửa chuỗi kết nối cơ sở dữ liệu. 
  - Mở CMD trỏ vào đường dẫn project eQACoLTD.Data và gõ lệnh: 
    dotnet tool install --global dotnet-ef (Cài đặt EF Tools)
    dotnet ef database update (Migrations Database)
  - Mở thư mục Scripts trong project eQACoLTD.Data và mở file Procedures.sql, chọn tới Database đã migration ở bước trên có tên là: DB_eQaCoLTD và tiến hành chạy 
  file.
2. Cách chạy projects
  - Có 4 project chính là: AdminMvc (trang quản trị admin), ClientMvc(trang thương mại điện tử), BackendApi, IdentityServer (cấp token, validate token, quản lý users,
  quản lý roles, phân quyền).
  - Lưu ý: chọn vào từng project và đổi sang chế độ Kestrel Server, không run projects ở chế độ IIS (vì với các OS như Linux cần có trust HTTPS)
  - Để hệ thống chạy hoàn chỉnh cần setup debug multiple projects theo thứ tự: IdenittyServer->BackendApi->AdminMvc->ClientMvc hoặc nếu chỉ cần test API không cần
  giao diện thì chỉ cần chạy 2 projects: IdentityServer->BackendApi
  - Luồng đi của hệ thống: 
  BackendApi là API chứa toàn bộ nghiệp vụ của hệ thống chạy tại địa chỉ: https://localhost:5001/swagger (Sử dụng Swagger Document)
  IdentityServer: Sử dụng IdentityServer4 để bảo vệ API, cấp token cho user, validate token, đăng nhập, đăng ký, đăng xuất, login bằng third party: facebook, google,
  phân quyền,... chạy tại địa chỉ: https://localhost:5000
  AdminMvc (https://localhost:5002),ClientMvc (https://localhost:5003): chỉ chứa giao diện, không có code xử lý logic, đơn giản chỉ là gọi API và cập nhật giao diện. Tài khoản admin và employees nằm trong thư mục:
  eQaCoLTD.Data->Configurations->AppUserConfiguration.cs
