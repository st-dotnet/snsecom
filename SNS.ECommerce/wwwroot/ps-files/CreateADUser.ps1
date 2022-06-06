param (
    [string]$FirstName = $(throw "-FirstName is required."),
    [string]$LastName = $(throw "-LastName is required."),
    [string]$Email = $(throw "-Email is required."),
    [string]$Password = $(throw "-Password is required."),
    [string]$Group = $(throw "-Group is required.")
)
Set-ExecutionPolicy Unrestricted
write-output "Email is $Email" 