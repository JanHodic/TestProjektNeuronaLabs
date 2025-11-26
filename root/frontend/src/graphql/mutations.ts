// src/graphql/mutations.ts
import { gql } from "@apollo/client";

export const CREATE_PATIENT = gql`
  mutation CreatePatient($input: CreatePatientInput!) {
    createPatient(input: $input) {
      id
      fullName
      age
      lastDiagnosis
    }
  }
`;

export const ADD_DIAGNOSTIC_ENTRY = gql`
  mutation AddDiagnosticEntry($input: UpdateDiagnosticInput!) {
    addDiagnosticEntry(input: $input) {
      id
      fullName
      age
      lastDiagnosis
    }
  }
`;
