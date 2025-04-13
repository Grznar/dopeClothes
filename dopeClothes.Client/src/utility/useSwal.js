import Swal from "sweetalert2";
export function useSwal() {
  const showAlert = async (options) => {
    return await Swal.fire(options);
  };

  const showSuccess = async (message) => {
    return await showAlert({
      position: "center",
      showConfirmButton: false,
      timer: 2500,
      title: "Success",
      text: message,
      icon: "success",
    });
  };
  const showError = async (message) => {
    return await showAlert({
      position: "center",
      showConfirmButton: false,
      timer: 2500,
      title: "Error encountered",
      text: message,
      icon: "error",
    });
  };
  const showConfirm = async (message) => {
    return await showAlert({
      title: "Are you sure?",
      text: message,
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!",
    });
  };
  return { showConfirm, showError, showSuccess };
}
