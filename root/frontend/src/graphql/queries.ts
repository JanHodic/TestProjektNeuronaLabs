import { gql } from "@apollo/client";

export const GET_PATIENTS = gql`
  query GetPatients {
    patients {
      id
      fullName
      age
      lastDiagnosis
    }
  }
`;

export const GET_PATIENT_DETAIL = gql`
  query GetPatientById($id: Int!) {
    patientById(id: $id) {
      id
      fullName
      age
      lastDiagnosis
      diagnosticEntries {
        id
        recordedAt
        diagnosis
        notes
      }
    }
  }
`;
