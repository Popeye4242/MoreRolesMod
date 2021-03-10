pipeline {
  agent 'any'
  environment {
    AmongUs = "${workspace}\\Among Us"
    CurrentModVersion = '1.0.0'
    CurrentAmongUsVersion = 'v2021.3.5s'
  }
  stages {
    stage('Prepare Workspace') {
      steps {
        powershell "cp -r \"C:\\Among Us ${CurrentAmongUsVersion}\" \"Among Us\""
      }
    }
    stage('Build') {
      steps {
        dir('src') {
          bat "\"${tool 'dotnet'}dotnet\" restore MoreRolesMod.sln"
          bat "\"${tool 'dotnet'}dotnet\" build MoreRolesMod.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=${CurrentModVersion}.${env.BUILD_NUMBER}"
        }
      }
    }
    stage('Make Installer') {
      steps {
        dir('installer') {
          bat "\"${tool 'Advanced Installer 18.0'}AdvancedInstaller.com\" /edit \"More Roles Mod Setup.aip\" /SetVersion ${CurrentModVersion}.${env.BUILD_NUMBER}"
          bat "\"${tool 'Advanced Installer 18.0'}AdvancedInstaller.com\" /build \"More Roles Mod Setup.aip\""
        }
        dir ('installer-output') {
          archiveArtifacts artifacts: 'More Roles Mod Setup.exe', excludes: ''
        }
      }
    }
  }
}
