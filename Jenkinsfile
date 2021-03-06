pipeline {
  agent {
    label 'Windows Agent'
  }
  environment {
    AmongUs = "${workspace}\\Among Us"
    AMONG_US_KEY = credentials('AmongUsKey')
    CurrentVersion = '1.0.0'
  }
  stages {
    stage('Prepare Workspace') {
      steps {
        powershell 'cp -r \"C:\\Among Us\" \"Among Us\"'
      }
    }
    stage('Build') {
      steps {
        dir('src') {
          bat 'dotnet restore MoreRolesMod.sln'
          bat "dotnet build MoreRolesMod.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=${CurrentVersion}.${env.BUILD_NUMBER}"
        }
      }
    }
    stage('Make Installer') {
      steps {
        dir('installer') {
          bat "\"${tool 'Advanced Installer 18.0'}\" /edit \"More Roles Mod Setup.aip\" /SetVersion ${CurrentVersion}.${env.BUILD_NUMBER}"
          bat "\"${tool 'Advanced Installer 18.0'}\" /build \"More Roles Mod Setup.aip\""
        }
        dir ('installer-output') {
          archiveArtifacts artifacts: 'More Roles Mod Setup.exe', excludes: ''
        }
      }
    }
  }
}
