/* eslint-disable react/prop-types */
import {
	Button,
	Modal,
	ModalBody,
	ModalContent,
	ModalFooter,
	ModalHeader,
} from "@nextui-org/react";

export default function ModalForm({ isOpen, title, children, setSubmit }) {
	return (
		<Modal
			isOpen={isOpen}
			onOpenChange={() => setSubmit(true)}
			placement="top-center"
			isDismissable={false}
			isKeyboardDismissDisabled={true}
			size="2xl"
			backdrop="blur"
		>
			<ModalContent>
				{() => (
					<>
						<ModalHeader className="flex flex-col gap-1">{title}</ModalHeader>
						<ModalBody>{children}</ModalBody>
						<ModalFooter>
							<Button
								color="danger"
								variant="flat"
								onPress={() => {
									setSubmit(true);
								}}
							>
								Cerrar
							</Button>
						</ModalFooter>
					</>
				)}
			</ModalContent>
		</Modal>
	);
}
