Param(
    [string]$ProjectPath = "Api/AeroVelozDesktop.Api/AeroVelozDesktop.Api.csproj"
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path $ProjectPath)) {
    throw "No se encontró el proyecto API en '$ProjectPath'. Ejecuta el script desde la raíz del repo o pasa -ProjectPath."
}

function Set-Secret([string]$Key, [string]$Value) {
    if ([string]::IsNullOrWhiteSpace($Value)) {
        Write-Host "Saltando $Key (valor vacío)"
        return
    }

    dotnet user-secrets set $Key $Value --project $ProjectPath | Out-Null
    Write-Host "OK: $Key"
}

Write-Host "Configuración de User Secrets para SMTP (proyecto: $ProjectPath)"
Write-Host "Los valores se guardan localmente y NO se commitean al repo."

$hostValue = Read-Host "Smtp:Host (ej. smtp.gmail.com)"
$portValue = Read-Host "Smtp:Port (default 587)"
if ([string]::IsNullOrWhiteSpace($portValue)) { $portValue = "587" }
$userValue = Read-Host "Smtp:UserName (ej. tu correo)"

$pwdSecure = Read-Host "Smtp:Password (no se mostrará)" -AsSecureString
$pwdBstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($pwdSecure)
try {
    $pwdValue = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($pwdBstr)
}
finally {
    [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($pwdBstr)
}

$fromAddressValue = Read-Host "Smtp:FromAddress (remitente)"
$fromNameValue = Read-Host "Smtp:FromName (default AeroVeloz)"
if ([string]::IsNullOrWhiteSpace($fromNameValue)) { $fromNameValue = "AeroVeloz" }
$sslValue = Read-Host "Smtp:EnableSsl (true/false, default true)"
if ([string]::IsNullOrWhiteSpace($sslValue)) { $sslValue = "true" }

# Inicializar User Secrets si hiciera falta (no afecta si ya existe)
dotnet user-secrets init --project $ProjectPath | Out-Null

Set-Secret "Smtp:Host" $hostValue
Set-Secret "Smtp:Port" $portValue
Set-Secret "Smtp:UserName" $userValue
Set-Secret "Smtp:Password" $pwdValue
Set-Secret "Smtp:FromAddress" $fromAddressValue
Set-Secret "Smtp:FromName" $fromNameValue
Set-Secret "Smtp:EnableSsl" $sslValue

Write-Host "Listo. Reinicia la API y vuelve a probar el registro del aeropuerto."