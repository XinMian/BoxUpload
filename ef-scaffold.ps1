cd ApplicationCore
dotnet ef dbcontext -v scaffold "Server=.;Database=Box;user id=sa;password=P@ssw0rd" Microsoft.EntityFrameworkCore.SqlServer -c BoxContext -o "Entities" -f --no-build